using Backend.Common;
using Backend.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Sevices;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace TestBackend.ServicesTest
{
    public class AuthServiceTest
    {
        private readonly IAuthRepository _authRepo;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthServiceTest() 
        {
            _authRepo = A.Fake<IAuthRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _configuration = A.Fake<IConfiguration>();
        }

        [Fact]
        public  async Task AuthService_LoginAsyn_InvalidPassword()
        {
            //Arrange
            AuthService _authService = new AuthService(_userRepository, _configuration, _authRepo);
            var request = new UserLoginDto() { 
                Email = "a@gmail.com", 
                Password = "123@abc" };
            var userCheckMail = new User() { 
                Email = "a@gmail.com", 
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "123@abcA")
            };
            A.CallTo(()=> _authRepo.FindUserByEmail(request.Email)).Returns(userCheckMail);
            A.CallTo( () => _configuration["appsetting:token"]).Returns("secret_key");
            A.CallTo( () => _configuration["appsetting:issuer"]).Returns("appsetting:issuer");
            A.CallTo( () => _configuration["appsetting:audience"]).Returns("appsetting:audience");
            A.CallTo( () => _configuration["appsetting:addMinu"]).Returns("1");
            A.CallTo( () => _configuration["appsetting:timeResfreshTokenExpire"]).Returns("1");

            //Act
            var result = await _authService.LoginAsyn(request);

            //Assert
            result.Should().NotBeNull();
            result.errorCode.Should().Be(ErrorCode.InvalidInput);

        }
    }
}