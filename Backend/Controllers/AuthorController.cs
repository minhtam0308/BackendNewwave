using Backend.Entities;
using Backend.Exceptions;
using Backend.Interface.Service;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
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
                throw new FEException("Provide Name");
            }
            var result = await authorServices.PostAuthor(name);
            if(result == 0)
            {
                return Ok(new {
                EC = 0,
                EM = "Create success"
                });
            }
            return StatusCode(500, new
            {
                EC = 1,
                EM = "ERROR FROM BE"
            });
        }


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
            
            if(author is null || author.NameAuthor == "")
                throw new FEException("Author is undefined");


            var result = await authorServices.PutAuthor(author);
            if (result == 2)
            throw new FEException("Author is not exist");

            if (result == 0) 
                return Ok(new{
                    EC = 0,
                    EM = "Edit success"
                });
            return StatusCode(500, new
            {
                EC = 1,
                EM = "ERROR FROM BE"
            });

        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteAuthor")]
        public async Task<ActionResult> DeleteAuthor(Guid idAuthor)
        {
            var result = await authorServices.DeleteAuthor(idAuthor);
            if (result == 2)
            {
                throw new FEException("Author is not exist");
            }
            if(result == 0)
                return Ok(new
                {
                    EC = 0,
                    EM = "Delete success"
                });
            return StatusCode(500, new
            {
                EC = 1,
                EM = "ERROR FROM BE"
            });
        }

    }
}
