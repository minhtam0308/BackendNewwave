using Backend.Common;
using Backend.DTOs;
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

            var resultDeteleCartBook = cartBookServices.DeleteCartBook(idCartGuid, idBookGuid, userId);
            if (resultDeteleCartBook.errorCode != ErrorCode.Success)
                return BadRequest(resultDeteleCartBook);

            return Ok(resultDeteleCartBook);
        }

        [Authorize(Roles = "user, admin")]
        [HttpPut("putChangeQuantityCartBook")]
        public ActionResult PutChangeQuantityCartBook(ChangeQuantityCartBookRequest request)
        {
            if(request == null)
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));

            if (!Guid.TryParse(request.IdCart, out Guid idCartGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));

            if (!Guid.TryParse(request.IdBook, out Guid idBookGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdReuslt))
                return BadRequest(_responseDto.GenerateStrategyResponseDto(ErrorCode.InvalidInput));

            var resultChangQuantity = cartBookServices.PutQuantityCartBook(idCartGuid, idBookGuid, request.Quantity, userId);
            if (resultChangQuantity.errorCode != ErrorCode.Success)
                return BadRequest(resultChangQuantity);

            return Ok(resultChangQuantity);
        }

    }
}
