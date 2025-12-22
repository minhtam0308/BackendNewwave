using AutoMapper;
using Backend.DTOs;
using Backend.Interface.IRepositories;
using Backend.Interface.IServices;
using Backend.Repositories;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BorrowServices : BaseService<Borrow>, IBorrowServices
    {
        private readonly IBorrowRepository _borrowRepo;
        private readonly ResponseDto _response = new ResponseDto();
        private readonly IMapper _mapper;
        public BorrowServices(IBorrowRepository borrowRepo, IMapper mapper) : base(borrowRepo)
        {
            _mapper = mapper;
            _borrowRepo = borrowRepo;
        }

        public ResponseDto BorrowBook(Guid userId, Borrow request, int numberBorrowDate)
        {
            // Validate request
            if (request.ExpiresBorrow <= DateTime.Now)
                return _response.GenerateStrategyResponseDto(Common.ErrorCode.InvalidInput);

            request.ExpiresReturn = request.ExpiresBorrow.AddDays(numberBorrowDate);
            request.IdUser = userId;

            _borrowRepo.Insert(request, userId.ToString());
            _borrowRepo.SaveChanges();

            return _response.GenerateStrategyResponseDto(Common.ErrorCode.Success);
        }

    }
}
