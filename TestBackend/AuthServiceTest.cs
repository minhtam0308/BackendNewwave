using Backend.Common;
using BeNewNewave.DTOs;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Sevices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;

namespace TestBackend
{
    public class AuthServiceTest
    {
        private readonly ResponseDto _responseDto = new ResponseDto();
        private readonly Mock<IAuthorRepository> _authRepo;
        private readonly AuthService _authService;

        //public AuthServiceTests(IConfiguration configuration)
        //{
        //    _authRepo = new Mock<IAuthorRepository>();
        //    _authService = new AuthService(configuration, _authRepo.Object);
        //}
        [Fact]
        public  void LoginAsyncPassingTest()
        {
            var loginAsyncPassingTest = new Mock<IAuthService>();
            loginAsyncPassingTest.Setup(x => x.LoginAsyn(new UserLoginDto() { Email = "", Password = "" }))
                .ReturnsAsync( _responseDto.GenerateStrategyResponseDto(ErrorCode.Success));



            //Assert.Equal(_responseDto.GenerateStrategyResponseDto(ErrorCode.Success),
            //    loginAsyncPassingTest.Object.LoginAsyn(new UserLoginDto() { Email = "a@gmail.com", Password = "123@abcA" }));
        }
    }
}