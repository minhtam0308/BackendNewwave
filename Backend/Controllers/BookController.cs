using Backend.Models;
using Backend.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookServices bookServices) : ControllerBase
    {
        [HttpGet("getAllBook")]
        public async Task<ActionResult<List<BookResponse>>> GetAllBook()
        {
            var lstBook = await bookServices.GetAllBook();
            if (lstBook is null)
                return Ok("Have no book");
            return Ok(lstBook);
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

    }
}
