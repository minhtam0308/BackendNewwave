
using Azure.Core;
using Backend.Common;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.ComponentModel;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace BeNewNewave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookServices bookServices) : ControllerBase
    {

        private readonly ResponseDto _responseDto = new ResponseDto();
        [HttpGet("getAllBook")]
        public ActionResult<ResponseDto> GetAllBook()
        {
            var lstBook = bookServices.GetAllBook();
            _responseDto.SetResponseDtoStrategy(new Success("get success", lstBook));
            return Ok(_responseDto.GetResponseDto());
        }

        [HttpGet("getPagedBook")]
        public async Task<ActionResult> GetPagedBookAsync([FromQuery] PaginationRequest request)
        {
            var inforPage = await bookServices.GetBookPaginateAsync(request);
            return Ok(inforPage);
        }

        [HttpPost("postCreateBook")]
        [Authorize(Roles = "admin")]
        public ActionResult<ResponseDto> PostCreateBook(BookRequest request)
        {
            if (request is null || request.TotalCopies == 0 || request.Title == "")
            {
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));
            }
            //get idUser
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));
            //create book
            var resultCreate = bookServices.PostCreateBook(request, userId);
            return Ok(resultCreate);
        }

        [HttpPut("putBook")]
        [Authorize(Roles = "admin")]
        public ActionResult<ResponseDto> PutBook(Book request)
        {
            //get idUser
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));
            var resultPutBook = bookServices.PutBook(request, userId);
            return Ok(resultPutBook);
        }

        [HttpDelete("delBook")]
        [Authorize(Roles = "admin")]
        public ActionResult<ResponseDto> DelBook(string idBook)
        {
            if (!Guid.TryParse(idBook, out var guidId))
            {
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));
            }

            //get idUser
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));
            bookServices.Delete(guidId, userId);
            return Ok(_responseDto.GenerateStrategyResponseDto(ErrorCode.Success));

        }

        [HttpPost("addByExcel")]
        [Authorize(Roles = "admin")]
        public ActionResult<ResponseDto> AddBookByExcel(IFormFile file)
        {
            if(file == null)
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var stream = new MemoryStream();
            file.CopyTo(stream);

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];

            int rowCount = worksheet.Dimension.Rows;

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));

            for (int row = 2; row <= rowCount; row++)
            {
                var title = worksheet.Cells[row, 1].Text?.Trim();

                if (string.IsNullOrEmpty(title))
                    continue; 
                var book = new BookRequest
                {
                    Title = title,
                    Description = worksheet.Cells[row, 2].Text,
                    IdAuthor = Guid.Parse(worksheet.Cells[row, 3].Text),
                    TotalCopies = int.Parse(worksheet.Cells[row, 4].Text),
                    UrlBook = worksheet.Cells[row, 5].Text
                };
                bookServices.PostCreateBook(book, userId);
            }

            return _responseDto.GenerateStrategyResponseDto(ErrorCode.Success);

        }

    }
}
