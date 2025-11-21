using Backend.Models;
using Backend.Sevices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookServices bookServices) : ControllerBase
    {
        [HttpGet("getAllBook")]
        public async Task<ActionResult<List<BookResponse>>> getAllBook()
        {
            var lstBook = await bookServices.GetAllBook();
            if (lstBook is null)
                return Ok("Have no book");
            return Ok(lstBook);
        }
    }
}
