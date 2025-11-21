using Backend.Entitise;
using Backend.Models;

namespace Backend.Sevices
{
    public interface IAuthorServices
    {
        Task<string?> PostAuthor(string name);
        Task<List<Author>> getAllAuthor();
    }
}
