using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Sevices
{
    public class BookServices(AppDbContext context) : IBookServices
    {
        public async Task<List<BookResponse>?> GetAllBook()
        {
            var books = await context.Books
            .Include(b => b.Author)
            .Select(b => new BookResponse
            {
                Id = b.Id,
                Title = b.Title,
                NameAuthor = b.Author!.NameAuthor,
                Description = b.Description,
                TotalCopies = b.TotalCopies,
                AvailableCopies = b.AvailableCopies,
                Image = b.Image,
                IdAuthor = b.IdAuthor,
                CreatedAt = b.CreatedAt
            }).ToListAsync();
            if(books.Count == 0)
            {
                return null;
            }
            return books;
        }

        public Task<string> PostCreateBook()
        {
            throw new NotImplementedException();
        }
    }
}
