
using Backend.Data;
using Backend.Entitise;
using Serilog;

namespace Backend.Sevices
{
    public class AuthorServices(AppDbContext context) : IAuthorServices
    {
        public Task<List<Author>> getAllAuthor()
        {
            throw new NotImplementedException();
        }

        public async Task<string?> PostAuthor(string name)
        {
            Author newAuthor = new Author() { NameAuthor = name};
            context.Authors.Add(newAuthor);
            var test = context.SaveChanges();

            Log.Information("test {t}", test);
            return "Create success";
        }
    }
}
