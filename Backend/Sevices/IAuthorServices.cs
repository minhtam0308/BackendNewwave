using Backend.Entitise;
using Backend.Models;

namespace Backend.Sevices
{
    public interface IAuthorServices
    {
        Task<string?> PostAuthor(string name);
        Task<List<Author>> getAllAuthor();
        Task<string?> PutAuthor(AuthorRenameRequest author);
        Task<string?> DeleteAuthor(Guid id);
    }
}
