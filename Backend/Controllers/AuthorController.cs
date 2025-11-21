using Backend.Models;
using Backend.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(IAuthorServices authorServices) : ControllerBase
    {
        [HttpPost("postAuthor")]
        public async Task<ActionResult<string>> PostCreateAuthor(string? name)
        {
            if(name == null)
            {
                return BadRequest("Provide Name");
            }
            var result = await authorServices.PostAuthor(name);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("getAllAuthor")]
        //public async Task<ActionResult<string>> GetAllAuthor()
        //{


        //    return Ok(result);
        //}
    }
}
