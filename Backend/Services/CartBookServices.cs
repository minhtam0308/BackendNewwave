using Backend.Interface.IServices;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Services;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class CartBookServices : BaseService<CartBook>, ICartBookServices
    {
        private readonly ICartBookRepository _cartBookRepository;
        private readonly ResponseDto _response = new ResponseDto();
        public CartBookServices(ICartBookRepository repo) : base(repo) 
        { 
            _cartBookRepository = repo;
        }
        public ResponseDto DeleteCartBook(Guid idCart, Guid idBook, string idUser) 
        { 
            var cartBook = _cartBookRepository.GetByIdBookAndIdCart(idCart, idBook);
            if (cartBook == null)
                return _response.GenerateStrategyResponseDto(Common.ErrorCode.InvalidInput);
            _cartBookRepository.Delete(cartBook, idUser);
            return _response.GenerateStrategyResponseDto(Common.ErrorCode.Success);
        }
    }
}
