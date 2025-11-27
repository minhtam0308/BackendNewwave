using Backend.Entitise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.SymbolStore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using Backend.Models;
using Backend.Sevices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IConfiguration configuration) : ControllerBase
    {
       
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDto request)
        {
            var user = await authService.RegisterAsyn(request);
            if(user == null)
            {
                return BadRequest("Email has already exist");
            }
            return Ok("Sign up success");
        }



        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserLoginDto request)
        {
            var result = await authService.LoginAsyn(request);
            if (result == null) {
                return Ok(new
                {
                    EC = 2,
                    EM = "Email or password is invalied"
                });
            }
            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"))
            });

            // 6. Lưu userId vào cookies
            Response.Cookies.Append("userId", result.User.Id.ToString(), new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
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
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequest request)
        {
            var result = await authService.RefreshTokenAsyn(request);
            if(result is null)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Refresh token is wrong"
                });
            }
            
            return Ok(result);
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

    }
}
