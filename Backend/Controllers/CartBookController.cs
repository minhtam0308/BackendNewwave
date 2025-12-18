using Backend.Common;
using Backend.Interface.IServices;
using Backend.Sevices;
using BeNewNewave.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartBookController(ICartBookServices cartBookServices) : ControllerBase
    {
        private readonly ResponseDto _responseDto = new ResponseDto();
        [Authorize(Roles = "user, admin")]
        [HttpDelete("deleteBookCart")]
        public ActionResult DeleteBookCart(string idCart, string idBook)
        {

            if (!Guid.TryParse(idCart, out Guid idCartGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));

            if (!Guid.TryParse(idBook, out Guid idBookGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdReuslt))
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));

            var resultBorrow = cartBookServices.DeleteCartBook(idCartGuid, idBookGuid, userId);
            if (resultBorrow.errorCode != ErrorCode.Success)
                return BadRequest(resultBorrow);

            return Ok(resultBorrow);
        }

    }
}
