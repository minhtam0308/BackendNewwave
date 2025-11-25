using Backend.Entitise;
using Backend.Models;

namespace Backend.Sevices
{
    public interface IAuthorServices
    {
        Task<int?> PostAuthor(string name);
        Task<List<Author>> getAllAuthor();
        Task<int?> PutAuthor(AuthorRenameRequest author);
        Task<int?> DeleteAuthor(Guid id);
    }
}
