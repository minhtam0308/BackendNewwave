using Backend.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IServices;

namespace Backend.Interface.IServices
{
    public interface IDetailBorrowServices : IBaseService<DetailBorrow>
    {
        ResponseDto BorrowBook(Guid userId, AddBookToCartRequest request, Guid idBorrow);
    }
}
