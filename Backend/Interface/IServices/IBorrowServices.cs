using Backend.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IServices;

namespace Backend.Interface.IServices
{
    public interface IBorrowServices : IBaseService<Borrow>
    {
        ResponseDto BorrowBook(Guid userId, Borrow request, int numberBorrowDate);
    }
}
