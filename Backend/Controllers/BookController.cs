using Backend.Entitise;
using Backend.Models;
using Backend.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookServices bookServices) : ControllerBase
    {
        [HttpGet("getAllBook")]
        public async Task<ActionResult> GetAllBook()
        {
            var lstBook = await bookServices.GetAllBook();
            return Ok(new
            {
                ec = 0,
                em = lstBook
            });
        }

        [HttpPost("postCreateBook")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> PostCreateBook(BookRequest request)
        {
            var resultCreate = await bookServices.PostCreateBook(request);
            if (resultCreate == 1)
                return Ok(new
                {
                    EC = 1,
                    EM = "Error from BE"
                });
            return Ok(new
            {
                EC = 0,
                EM = "Create success"
            });
        }

        [HttpPut("putBook")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> PutBook(Book request)
        {
            var result = await bookServices.PutBook(request);
            if (result == 1)
                return Ok(new
                {
                    EC = 1,
                    EM = "Error from BE"
                });
            if(result == 2)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Book is not exist"
                });
            }

            return Ok(new
            {
                EC = 0,
                EM = "Edit success"
            });
        }

        [HttpDelete("delBook")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DelBook(string idBook)
        {
            if (!Guid.TryParse(idBook, out var guidId))
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Guid Wrong"
                });
            }
            var result = await bookServices.DelBook(guidId);
            if (result == 1)
                return Ok(new
                {
                    EC = 1,
                    EM = "Error from BE"
                });
            if (result == 2)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Book is not exist"
                });
            }

            return Ok(new
            {
                EC = 0,
                EM = "Delete success"
            });
        }

    }
}
