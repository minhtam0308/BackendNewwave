using Azure.Core;
using Backend.Data;
using Backend.Entities;
using Backend.Interface.Service;
using Backend.Migrations.ImageDB;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Backend.Sevices
{
    public class BookServices(AppDbContext context) : IBookServices
    {
        public async Task<int> DelBook(Guid idBook)
        {
            try
            {

                var bookDel = context.Books.FirstOrDefault(b => b.Id == idBook);
                if (bookDel == null)
                {
                    return 2;
                }
                context.Books.Remove(bookDel);
                await context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Information("Error PutBook {t}", ex);

                return 1;
            }

        }

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
                UrlBook = b.UrlBook,
                IdAuthor = b.IdAuthor,
                CreatedAt = b.CreatedAt
            }).ToListAsync();

            if (books.Count == 0)
            {
                return null;
            }
            return books;
        }

        public async Task<PagedResult?> GetBookPaginate(PaginationRequest paginationRequest)
        {
            try
            {
                var query = context.Books.AsQueryable();
                var totalCount = await query.CountAsync();

                if ((paginationRequest.PageNumber - 1) * paginationRequest.PageSize > totalCount)
                {
                    return null;
                }
                var items = await query
                    .Select(b => new BookResponse
                    {
                        Id = b.Id,
                        Title = b.Title,
                        NameAuthor = b.Author!.NameAuthor,
                        Description = b.Description,
                        TotalCopies = b.TotalCopies,
                        AvailableCopies = b.AvailableCopies,
                        UrlBook = b.UrlBook,
                        IdAuthor = b.IdAuthor,
                        CreatedAt = b.CreatedAt
                    })
                    .OrderBy(x => x.Id)
                    .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                    .Take(paginationRequest.PageSize)
                    .ToListAsync();

                var result = new PagedResult()
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = paginationRequest.PageNumber,
                    PageSize = paginationRequest.PageSize
                };

                return result;
            }
            catch (Exception ex)
            {
                Log.Information("Error GetBookPaginate {t}", ex);

                return null;
            }

        }

        public async Task<int> PostCreateBook(BookRequest request)
        {
            try
            {
                var bookNew = new Book()
                {
                    Title = request.Title,
                    IdAuthor = request.IdAuthor,
                    Description = request.Description,
                    AvailableCopies = request.TotalCopies,
                    TotalCopies = request.TotalCopies,
                    UrlBook = request.UrlBook
                };
                context.Books.Add(bookNew);
                await context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex) {
                Log.Information("Error PostCreateBook {t}", ex);

                return 1;
            }


        }

        public async Task<int> PutBook(Book request)
        {
            try
            {
                var bookEdit = context.Books.FirstOrDefault(b => b.Id == request.Id);
                if (bookEdit == null) {
                    return 2;
                }
                bookEdit.Title = request.Title;
                bookEdit.IdAuthor = request.IdAuthor;
                bookEdit.Description = request.Description;
                bookEdit.AvailableCopies = request.AvailableCopies;
                bookEdit.TotalCopies = request.TotalCopies;
                bookEdit.UrlBook = request.UrlBook;
                await context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Information("Error PutBook {t}", ex);

                return 1;
            }


        }
    }
}
