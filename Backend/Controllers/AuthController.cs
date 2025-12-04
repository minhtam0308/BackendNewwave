using Backend.Entities;
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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Backend.Interface.Service;

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
            if(user == 2)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Email was not validated!"
                });
            }
            if(user is null)
            {
                return Ok(new {
                EC = 2,
                EM = "Account is exist"
                });
            }
            return Ok(new
            {
                EC = 0,
                EM = "Register success"
            });
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
                return Ok(new
                {
                    EC = 2,
                    EM = "Refresh fail"
                });
            }

            if (!Guid.TryParse(userId, out var guidId))
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Guid can not convert"
                });
            }
            var result = await authService.RefreshTokenAsyn(new RefreshTokenRequest() { UserId = guidId, RefreshToken = refreshToken });
            if(result is null)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Refresh token is wrong"
                });
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
