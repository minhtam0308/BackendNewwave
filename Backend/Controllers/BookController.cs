using Backend.Entities;
using Backend.Exceptions;
using Backend.Interface.Service;
using Backend.Models;
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

        [HttpGet("getPagedBook")]
        public async Task<ActionResult> GetPagedBook([FromQuery]PaginationRequest request)
        {
            var inforPage = await bookServices.GetBookPaginate(request);
            if(inforPage is null)
            {
                throw new FEException("Out of book");
            }
            return Ok(new
            {
                ec = 0,
                em = inforPage
            });
        }

        [HttpPost("postCreateBook")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> PostCreateBook(BookRequest request)
        {
            if(request is null || request.TotalCopies == 0 || request.Title == "")
            {
                throw new FEException("Your data does not meet the requirements");
            }

            var resultCreate = await bookServices.PostCreateBook(request);
            if (resultCreate == 1)
                return StatusCode(500, new
                {
                    EC = 1,
                    EM = "ERROR FROM BE"
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
                return StatusCode(500, new
                {
                    EC = 1,
                    EM = "ERROR FROM BE"
                });
            if (result == 2)
            {
                throw new FEException("Book is not exist");
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
                throw new FEException("Guid Wrong");
            }
            var result = await bookServices.DelBook(guidId);
            if (result == 1)
                return StatusCode(500, new
                {
                    EC = 1,
                    EM = "ERROR FROM BE"
                });
            if (result == 2)
            {
                throw new FEException("Book is not exist");
            }

            return Ok(new
            {
                EC = 0,
                EM = "Delete success"
            });
        }

    }
}
