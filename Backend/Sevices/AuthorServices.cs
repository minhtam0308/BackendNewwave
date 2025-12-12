
using Backend.Data;
using Backend.Entities;
using Backend.Interface.Service;
using Backend.Models.Author;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Backend.Sevices
{
    public class AuthorServices(AppDbContext context) : IAuthorServices
    {
        public async Task<int?> DeleteAuthor(Guid id)
        {
            try
            {
                var oldAuhtor = await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
                if (oldAuhtor == null)
                {
                    return 2;
                }
                context.Remove(oldAuhtor);
                context.SaveChanges();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Information("Error DeleteAuthor {t}", ex);
                return 1;
            }

        }

        public async Task<List<Author>> GetAllAuthor()
        {
            List<Author> listAuthor = await context.Authors.ToListAsync();
            return listAuthor;
        }

        public async Task<int?> PostAuthor( string name )
        {
            try
            {
                Author newAuthor = new Author() { NameAuthor = name };
                await context.Authors.AddAsync(newAuthor);
                var test = await context.SaveChangesAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Information("Error PostAuthor {t}", ex);
                return 1;
            }

        }

        public async Task<int?> PutAuthor(AuthorRenameRequest author)
        {
            try
            {
                var oldAuhtor = await context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
                if (oldAuhtor is null)
                {
                    return 2;
                }
                oldAuhtor.NameAuthor = author.NameAuthor;
                await context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Information("Error PutAuthor {t}", ex);
                return 1;
            }

        }
    }
}
