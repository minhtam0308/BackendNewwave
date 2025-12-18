using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IServices;

namespace Backend.Interface.IServices
{
    public interface ICartBookServices : IBaseService<CartBook>
    {
        ResponseDto DeleteCartBook(Guid idCart, Guid idBook, string idUser);
    }
}
