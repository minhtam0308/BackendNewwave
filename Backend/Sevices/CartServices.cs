using AutoMapper;
using Azure.Core;
using Backend.Data;
using Backend.Entities;
using Backend.Interface.Service;
using Backend.Models.Book;
using Backend.Models.Cart;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Net.WebSockets;
using System.Text.Json;

namespace Backend.Sevices
{
    public class CartServices(AppDbContext context, IMapper mapper) : ICartServices
    {


        public async Task<int> PostAddCart(AddBookToCartRequest request, Guid idUser)
        {
            try
            {
                if (!Guid.TryParse(request.IdBook, out Guid idBook))
                    return 2;

                var bookInfor = context.Books.AsNoTracking().FirstOrDefault(b => b.Id == idBook);
                if (bookInfor == null || bookInfor.AvailableCopies < request.Quantity)
                    return 2;

                var checkCart = await context.Carts.FirstOrDefaultAsync(c => c.idUser == idUser);
                if (checkCart == null)
                {
                    var newCart = new Cart() { idUser = idUser };
                    context.Carts.Add(newCart);
                    
                    var newCartBook = new CartBook() { IdCart = newCart.Id, IdBook = idBook, Quantity = request.Quantity };
                    context.CartBooks.Add(newCartBook);
                    await context.SaveChangesAsync();
                }
                else
                {
                    var cartBookExist = await context.CartBooks.FirstOrDefaultAsync(cb => cb.IdBook == idBook && cb.IdCart == checkCart.Id);
                    if (cartBookExist == null)
                    {
                        var newCartBook = new CartBook() { IdCart = checkCart.Id, IdBook = idBook, Quantity = request.Quantity };
                        context.CartBooks.Add(newCartBook);

                    }
                    else
                    {
                        cartBookExist.Quantity = cartBookExist.Quantity + request.Quantity;
                    }
                    await context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                Log.Information("Error PostAddCart {t}", ex);

                return 1;
            }
        }
        public async Task<AllCartResponse?> GetAllCart(Guid idUser)
        {
            try
            {
                var cart = await context.Carts
                    .Include(c => c.cartBooks)
                    .FirstOrDefaultAsync(c => c.idUser == idUser);
                if (cart == null)
                {
                    return null;
                }

                AllCartResponse result = new AllCartResponse()
                {
                    IdCart = cart.Id,
                    ListBook = new List<BookResponse>()
                };

                if(cart.cartBooks != null && cart.cartBooks.Count != 0)
                {
                    foreach(var cartBook in cart.cartBooks)
                    {
                        var booktemp = await context.Books.Include(b=>b.Author).FirstOrDefaultAsync(book => book.Id == cartBook.IdBook);
                        if (booktemp != null) {
                            BookResponse resultBook = new BookResponse()
                            {
                                Id = booktemp.Id,
                                Title = booktemp.Title ,
                                NameAuthor = booktemp.Author!.NameAuthor,
                                Description = booktemp.Description,
                                TotalCopies = booktemp.TotalCopies,
                                AvailableCopies = booktemp.AvailableCopies,
                                UrlBook = booktemp.UrlBook,
                                IdAuthor = booktemp.IdAuthor,
                                Quantity = cartBook.Quantity,
                                CreatedAt = booktemp.CreatedAt
                            };
                            result.ListBook.Add(resultBook);
                        }
                    }
                }

                // In ra JSON
                //Console.WriteLine();

                return result;
            }
            catch (Exception ex)
            {
                Log.Information("Error GetAllCart {t}", ex);

                return null;
            }
        }
    }
}
