using Backend.Exceptions;
using Backend.Interface.Service;
using Backend.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [Authorize(Roles = "admin, user")]
        [HttpPut("putChangeUser")]
        public async Task<ActionResult> PutChangUser(UserResponse request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            if(!Guid.TryParse(userId, out Guid userIdReuslt))
                throw new FEException("Guid Wrong");


            UserResponse userRequest = new UserResponse()
            {
                Id = userIdReuslt,
                Email = request.Email,
                Age = request.Age,
                Location = request.Location,
                Name = request.Name,
                Role = request.Role,
                urlUserImage = request.urlUserImage,
                Department = request.Department,
                Class = request.Class,
                PhoneNumber = request.PhoneNumber
            };
            var resultChange = await userService.PutChangeUser(userRequest);
            if (resultChange == 1)
            {
                return StatusCode(500, new
                {
                    EC = 1,
                    EM = "ERROR FROM BE"
                });
            }
            if (resultChange == 2)
            {
                throw new FEException("Your data Wrong");
            }
            return Ok(new
            {
                EC = 0,
                EM = "Change success"
            });


        }
    }
}
