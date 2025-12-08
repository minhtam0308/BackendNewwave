using Backend.Models.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Interface.Service
{
    public interface ICartServices
    {
        Task<int> PostAddCart(AddBookToCartRequest request, Guid idUser);
        Task<AllCartResponse?> GetAllCart(Guid idUser);
    }
}
