using Backend.Entities;
using Backend.Exceptions;
using Backend.Interface.Service;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IConfiguration configuration) : ControllerBase
    {
       
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDto request)
        {
            if (request == null || !ValidatePassword(request.Password) || !IsValidEmail(request.Email))
            {
                throw new FEException("Your data does not meet the requirements");
            }
            var user = await authService.RegisterAsyn(request);

            if(user is null)
            {
                throw new FEException("Account was exist");
            }
            return Ok(new
            {
                EC = 0,
                EM = "Register success"
            });
        }
        bool ValidatePassword(string password)
        {
            // Ít nhất 8 ký tự, 1 chữ hoa, 1 chữ thường, 1 số, 1 ký tự đặc biệt
            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }



        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserLoginDto request)
        {
            if (request == null || !ValidatePassword(request.Password) || !IsValidEmail(request.Email))
            {
                throw new FEException("Your data does not meet the requirements");
            }
            var result = await authService.LoginAsyn(request);
            if (result == null) {
                throw new FEException("Email or password is invalied");
            }
            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"))
            });

            // 6. Lưu userId vào cookies
            Response.Cookies.Append("userId", result.User.Id.ToString(), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"))
            });
            return Ok(new
            {
                EC = 0,
                EM = result.AccessToken,
                user = result.User
            });
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("userId", out string? userId) || !Request.Cookies.TryGetValue("refreshToken", out string? refreshToken))
            {
                throw new FEException("Refresh fail");
            }

            if (!Guid.TryParse(userId, out var guidId))
            {
                throw new FEException("Guid can not convert");
            }
            var result = await authService.RefreshTokenAsyn(new RefreshTokenRequest() { UserId = guidId, RefreshToken = refreshToken });
            if(result is null)
            {
                throw new FEException("Refresh token is wrong");
            }

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"))
            });

            // 6. Lưu userId vào cookies
            Response.Cookies.Append("userId", result.User.Id.ToString(), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"))
            });

            return Ok(new
            {
                EC = 0,
                EM = result.AccessToken,
                user = result.User
            });
        }


        [Authorize]
        [HttpGet]
        public ActionResult<String> AuthorTestEndpoint()
        {
            return Ok("you are authenticated");
        }

        [Authorize(Roles = "admin")]
        [HttpGet("admin-only")]
        public ActionResult<String> AuthorAdminOnlyEndpoint()
        {
            return Ok("you are admin");
        }

        [HttpGet("logout")]
        public async Task<ActionResult<TokenResponseDto>> Logout()
        {

            Response.Cookies.Append("refreshToken", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            // 6. Lưu userId vào cookies
            Response.Cookies.Append("userId", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            });
            return Ok(new
            {
                EC = 0,
                EM = "You were logout"
            });
        }

    }
}
