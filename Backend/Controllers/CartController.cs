using Backend.Exceptions;
using Backend.Interface.Service;
using Backend.Models.Auth;
using Backend.Models.Cart;
using Backend.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartServices cartServices) : ControllerBase
    {
        [Authorize(Roles = "user, admin")]
        [HttpPost("addBookToCart")]
        public async Task<ActionResult<string>> AddBookToCart(AddBookToCartRequest request)
        {
            //Console.WriteLine("quantity ", request.Quantity);
            if (request.IdBook is null)
                throw new FEException("Your data does not meet the requirements");

            if (request.Quantity <= 0)
                throw new FEException("Your data does not meet the requirements");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdReuslt))
                throw new FEException("Guid Wrong");

            var resultBorrow = await cartServices.PostAddCart(request, userIdReuslt);

            if (resultBorrow == 2)
                throw new FEException("Your data does not meet the requirements");

            if(resultBorrow == 1)
                return StatusCode(500, new
                {
                    EC = 1,
                    EM = "ERROR FROM BE"
                });
            return Ok(new
            {
                EC = 0,
                EM = "Add success"
            });
        }


        [Authorize(Roles = "user, admin")]
        [HttpGet("getAllCart")]
        public async Task<ActionResult> GetAllCart()
        {
            //Console.WriteLine("quantity ", request.Quantity);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdReuslt))
                throw new FEException("Guid Wrong");

            var resultGetCart = await cartServices.GetAllCart(userIdReuslt);

            if (resultGetCart == null)
                throw new FEException("Your data does not meet the requirements");

            return Ok(new
            {
                EC = 0,
                EM = resultGetCart
            });
        }
    }
}
