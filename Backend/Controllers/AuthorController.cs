using Backend.Entitise;
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
        [Authorize(Roles = "admin")]
        [HttpPost("postCreateAuthor")]
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
        [HttpGet("getAllAuthor")]
        public async Task<ActionResult<List<Author>>> GetAllAuthor()
        {
            var result = await authorServices.getAllAuthor();
            
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("putEditAuthor")]
        public async Task<ActionResult<string>> PutEditAuthor([FromBody] AuthorRenameRequest author)
        {
            var result = await authorServices.PutAuthor(author);
            if(result is null)
            {
                return BadRequest("Author is not exist");
            }
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteAuthor")]
        public async Task<ActionResult<string>> DeleteAuthor(Guid idAuthor)
        {
            var result = await authorServices.DeleteAuthor(idAuthor);
            if (result is null)
            {
                return BadRequest("Author is not exist");
            }
            return Ok(result);
        }

    }
}
