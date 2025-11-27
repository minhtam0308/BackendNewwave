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
        public async Task<ActionResult> PostCreateAuthor(string? name)
        {
            if(name == null)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Provide Name"
                });
            }
            var result = await authorServices.PostAuthor(name);
            if(result == 0)
            {
                return Ok(new {
                EC = 0,
                EM = "Create success"
                });
            }
            return Ok(new
            {
                EC = 1,
                EM = "Error from BE"
            });
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getAllAuthor")]
        public async Task<ActionResult> GetAllAuthor()
        {
            var result = await authorServices.getAllAuthor();
            
            return Ok(new {
                EC = 0,
                EM = result
            });
        }

        [Authorize(Roles = "admin")]
        [HttpPut("putEditAuthor")]
        public async Task<ActionResult> PutEditAuthor([FromBody] AuthorRenameRequest author)
        {
            if(author is null)
            {
                return Ok(new
                {
                    EC = 3,
                    EM = "Author is undefined"
                });
            }

                var result = await authorServices.PutAuthor(author);
                if (result == 2) 
                return Ok(new
                    {
                        EC = 2,
                        EM = "Author is not exist"
                    });
                
                if(result == 0) 
                return Ok(new{
                    EC = 0,
                    EM = "Edit success"
                });
                return Ok(new
                {
                    EC = 1,
                    EM = "Error from BE"
                });

        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteAuthor")]
        public async Task<ActionResult> DeleteAuthor(Guid idAuthor)
        {
            var result = await authorServices.DeleteAuthor(idAuthor);
            if (result == 2)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Author is not exist"
                });
            }
            if(result == 0)
                return Ok(new
                {
                    EC = 0,
                    EM = "Delete success"
                });
            return Ok(new
            {
                EC = 1,
                EM = "Error from BE"
            });
        }

    }
}
