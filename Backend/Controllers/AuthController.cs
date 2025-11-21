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
    public class AuthController(IAuthService authService) : ControllerBase
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
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var result = await authService.LoginAsyn(request);
            if (result == null) {
                return Unauthorized("Invalid email or password");
            }
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequest request)
        {
            var result = await authService.RefreshTokenAsyn(request);
            if(result is null)
            {
                return BadRequest("Need to login");
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
