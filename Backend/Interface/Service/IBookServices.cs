using Backend.Entities;
using Backend.Models.Book;

namespace Backend.Interface.Service
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
