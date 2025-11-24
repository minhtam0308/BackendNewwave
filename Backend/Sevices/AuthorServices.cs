
using Backend.Data;
using Backend.Entitise;
using Backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Backend.Sevices
{
    public class AuthorServices(AppDbContext context) : IAuthorServices
    {
        public async Task<string?> DeleteAuthor(Guid id)
        {
            var oldAuhtor = await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (oldAuhtor == null)
            {
                return null;
            }
            context.Remove(oldAuhtor);
            context.SaveChanges();
            return "Delete success";
        }

        public async Task<List<Author>> getAllAuthor()
        {
            List<Author> listAuthor = await context.Authors.ToListAsync();
            return listAuthor;
        }

        public async Task<string?> PostAuthor( string name )
        {
            Author newAuthor = new Author() { NameAuthor = name};
            await context.Authors.AddAsync(newAuthor);
            var test = await context.SaveChangesAsync();

            //Log.Information("test {t}", test);
            return "Create success";
        }

        public async Task<string?> PutAuthor(AuthorRenameRequest author)
        {
            var oldAuhtor = await context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
            if (oldAuhtor == null)
            {
                return null;
            }
            oldAuhtor.NameAuthor = author.NameAuthor;
            await context.SaveChangesAsync();
            return "Edit success";
        }
    }
}
