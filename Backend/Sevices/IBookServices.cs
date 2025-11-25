using Backend.Entitise;
using Backend.Models;

namespace Backend.Sevices
{
    public interface IBookServices
    {
        Task<List<BookResponse>?> GetAllBook();
        Task<int> PostCreateBook(BookRequest request);

    }
}
