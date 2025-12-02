using Backend.Entitise;
using Backend.Models;

namespace Backend.Sevices
{
    public interface IBookServices
    {
        Task<List<BookResponse>?> GetAllBook();
        Task<PagedResult?> GetBookPaginate(PaginationRequest paginationRequest);
        Task<int> PostCreateBook(BookRequest request);
        Task<int> PutBook(Book request);
        Task<int> DelBook(Guid idBook);

    }
}
