using Backend.Entities;
using Backend.Models.Author;

namespace Backend.Interface.Service
{
    public interface IAuthorServices
    {
        Task<int?> PostAuthor(string name);
        Task<List<Author>> GetAllAuthor();
        Task<int?> PutAuthor(AuthorRenameRequest author);
        Task<int?> DeleteAuthor(Guid id);
    }
}
