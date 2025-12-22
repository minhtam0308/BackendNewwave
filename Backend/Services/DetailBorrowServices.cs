using Backend.Common;
using Backend.DTOs;
using Backend.Interface.IRepositories;
using Backend.Interface.IServices;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class DetailBorrowServices : BaseService<DetailBorrow>, IDetailBorrowServices
    {
        private readonly IDetailBorrowRepository _detailBorrowRepo;
        private readonly IBookRepository _bookRepo;
        private readonly ResponseDto _response = new ResponseDto();

        public DetailBorrowServices(IDetailBorrowRepository detailBorrowRepo, IBookRepository bookRepo) : base(detailBorrowRepo)
        {
            _detailBorrowRepo = detailBorrowRepo;
            _bookRepo = bookRepo;
        }

        public ResponseDto BorrowBook(Guid userId, AddBookToCartRequest request, Guid idBorrow)
        {
            // Validate request
            if (request.Quantity <= 0)
                return _response.GenerateStrategyResponseDto(Common.ErrorCode.InvalidInput);

            if (!Guid.TryParse(request.IdBook, out Guid idBookGuid))
                return _response.GenerateStrategyResponseDto(ErrorCode.InvalidInput);

            // Find book
            var book = _bookRepo.GetById(idBookGuid);
            if (book == null)
                return _response.GenerateStrategyResponseDto(Common.ErrorCode.InvalidInput);

            // Check stock
            if (book.AvailableCopies < request.Quantity)
                return _response.GenerateStrategyResponseDto(Common.ErrorCode.InvalidInput);

            // Update stock
            book.AvailableCopies -= request.Quantity;

            // Create borrow record
            var detailBorrow = new DetailBorrow
            {
                IdBook = book.Id,
                Quantity = request.Quantity,
                IdBorrow = idBorrow
            };

            _detailBorrowRepo.Insert(detailBorrow, userId.ToString());
            _detailBorrowRepo.SaveChanges();

            return _response.GenerateStrategyResponseDto(Common.ErrorCode.Success);
        }
    }
}
