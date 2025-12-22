
using Azure.Core;
using Backend.Common;
using Backend.DTOs;
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

            if (worksheet.Cells[1, 1].Text?.Trim() != "Title" ||
                worksheet.Cells[1, 2].Text?.Trim() != "Description" ||
                worksheet.Cells[1, 3].Text?.Trim() != "AuthorId" ||
                worksheet.Cells[1, 4].Text?.Trim() != "TotalCopies" ||
                worksheet.Cells[1, 5].Text?.Trim() != "UrlBook")
            {
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));
            }

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

        [Authorize(Roles = "admin")]
        [HttpGet("export-excel")]
        public IActionResult ExportBooksToExcel()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Books");

            worksheet.Cells[1, 1].Value = "Title";
            worksheet.Cells[1, 2].Value = "Description";
            worksheet.Cells[1, 3].Value = "AuthorId";
            worksheet.Cells[1, 4].Value = "TotalCopies";
            worksheet.Cells[1, 5].Value = "UrlBook";

            var books = bookServices.GetAll();

            int row = 2;
            foreach (var book in books)
            {
                worksheet.Cells[row, 1].Value = book.Title;
                worksheet.Cells[row, 2].Value = book.Description;
                worksheet.Cells[row, 3].Value = book.IdAuthor.ToString();
                worksheet.Cells[row, 4].Value = book.TotalCopies;
                worksheet.Cells[row, 5].Value = book.UrlBook;
                row++;
            }

            worksheet.Cells.AutoFitColumns();

            var fileBytes = package.GetAsByteArray();
            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Books.xlsx"
            );
        }

        [Authorize(Roles = "admin")]
        [HttpGet("export-template")]
        public IActionResult ExportExcelTemplate()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Template");

            ws.Cells[1, 1].Value = "Title";
            ws.Cells[1, 2].Value = "Description";
            ws.Cells[1, 3].Value = "AuthorId";
            ws.Cells[1, 4].Value = "TotalCopies";
            ws.Cells[1, 5].Value = "UrlBook";

            ws.Cells.AutoFitColumns();

            return File(
                package.GetAsByteArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "BookTemplate.xlsx"
            );
        }

    }
}
