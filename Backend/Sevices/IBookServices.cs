using Backend.Entitise;
using Backend.Models;

namespace Backend.Sevices
{
    public interface IBookServices
    {
        Task<List<BookResponse>?> GetAllBook();
        Task<string> PostCreateBook();

    }
}
