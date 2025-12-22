using Backend.Interface.IServices;
using Backend.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Repositories;
using BeNewNewave.Services;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class CartBookServices : BaseService<CartBook>, ICartBookServices
    {
        private readonly ICartBookRepository _cartBookRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ResponseDto _response = new ResponseDto();
        public CartBookServices(ICartBookRepository cartBookRepo, IBookRepository bookRepo) : base(cartBookRepo) 
        { 
            _cartBookRepository = cartBookRepo;
            _bookRepository = bookRepo;
        }
        public ResponseDto DeleteCartBook(Guid idCart, Guid idBook, string idUser) 
        { 
            var cartBook = _cartBookRepository.GetByIdBookAndIdCart(idCart, idBook);
            if (cartBook == null)
                return _response.GenerateStrategyResponseDto(Common.ErrorCode.InvalidInput);
            _cartBookRepository.Delete(cartBook.Id, idUser);
            _cartBookRepository.SaveChanges();
            return _response.GenerateStrategyResponseDto(Common.ErrorCode.Success);
        }

        public ResponseDto PutQuantityCartBook(Guid idCart, Guid idBook,int quantity ,string idUser)
        {
            var cartBook = _cartBookRepository.GetByIdBookAndIdCart(idCart, idBook);
            if (cartBook == null)
                return _response.GenerateStrategyResponseDto(Common.ErrorCode.InvalidInput);

            var book = _bookRepository.GetById(idBook);

            if(book != null && book.AvailableCopies <  quantity)
                return _response.GenerateStrategyResponseDto(Common.ErrorCode.InvalidInput);

            cartBook.Quantity = quantity;
            _cartBookRepository.SaveChanges();
            return _response.GenerateStrategyResponseDto(Common.ErrorCode.Success);
        }
    }
}
