using Azure;
using Backend.Common;
using Backend.DTOs;
using Backend.Interface.IServices;
using BeNewNewave.Entities;
using BeNewNewave.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowBooksController(IBorrowServices borrowServices, IDetailBorrowServices detailBorrowServices) : ControllerBase
    {
        private ResponseDto _response = new ResponseDto();
        [Authorize(Roles = "admin, user")]
        [HttpPost("borrow-book")]
        public async Task<ActionResult<ResponseDto>> BorrowBook(BorrowBookRequest request)
        {
            if (request == null || request.NumberBorrowDate <= 0)
            {
                return _response.GenerateStrategyResponseDto(ErrorCode.InvalidInput);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_response.GenerateStrategyResponseDto(ErrorCode.InvalidInput));

            if(request.ListBooks == null)
                return BadRequest(_response.GenerateStrategyResponseDto(ErrorCode.InvalidInput));

            var borrowNew = new Borrow() { ExpiresBorrow = request.ExpiresBorrow };
            var resultBorrow = borrowServices.BorrowBook(userIdGuid, borrowNew, request.NumberBorrowDate);

            if(resultBorrow.errorCode != ErrorCode.Success)
                return BadRequest(resultBorrow);

            foreach (var book in request.ListBooks)
            {
                detailBorrowServices.BorrowBook(userIdGuid, book, borrowNew.Id);
            }

            return Ok(_response.GenerateStrategyResponseDto(ErrorCode.Success));
        }
    }
}
