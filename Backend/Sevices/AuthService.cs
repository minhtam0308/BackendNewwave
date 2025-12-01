using Backend.Data;
using Backend.Entitise;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Sevices
{
    public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<TokenResponseDto?> LoginAsyn(UserLoginDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                return null;

            return await CreateTokenRespon(user);
        }

        private async Task<TokenResponseDto> CreateTokenRespon(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsyn(user),
                User = new UserResponse { Email = user.Email , 
                    Id=user.Id, 
                    Role= user.Role, 
                    Name=user.Name, 
                    urlUserImage = user.urlUserImage!,
                    Age = user.Age,
                    Location = user.Location,
                
                }
            };
        }

        public async Task<User?> RegisterAsyn(UserDto request)
        {
            if (await context.Users.AnyAsync(u => u.Email == request.Email)) {
                return null;
            }
            var user = new User();
            var passwordHard = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.Email = request.Email;
            user.PasswordHash = passwordHard;
            user.Name = request.Name;
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        private String CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<String>("appsetting:token")));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["appsetting:token"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["appsetting:issuer"],
                audience: configuration["appsetting:audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(configuration.GetValue<int>("appsetting:addMinu")),
                signingCredentials: creds
                );
            //Log.Information("key {key}", new JwtSecurityTokenHandler().WriteToken(tokenDescriptor));
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            //Log.Information("randomNumber1 {a}", randomNumber);

            rng.GetBytes(randomNumber);

            //Log.Information("randomNumber2 {a}", randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsyn(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokeExpiryTime = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"));
            await context.SaveChangesAsync();
            return refreshToken;
        }


        public async Task<TokenResponseDto?> RefreshTokenAsyn(RefreshTokenRequest request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            //Log.Information("user {user}", user.Name);
            if (user is null || user.RefreshTokeExpiryTime < DateTime.UtcNow || user.RefreshToken != request.RefreshToken)
            {
                return null;
            }
            return await CreateTokenRespon(user);
        }
    }
}
