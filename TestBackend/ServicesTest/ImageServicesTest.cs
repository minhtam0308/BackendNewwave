using AutoMapper;
using Azure.Core;
using Backend.Common;
using Backend.Services;
using Backend.Sevices;
using Backend.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Sevices;
using FakeItEasy;
using FluentAssertions;
using System.Threading.Tasks;
namespace TestBackend.ServicesTest;


public class UserServicesTest
{
    private readonly IUserRepository _userRepository;

    public UserServicesTest()
    {
        _userRepository = A.Fake<IUserRepository>();
    }

    //update user success
    [Fact]
    public void UserServices_PutChangeUser_ReturnOK()
    {
        //Arrange
        var request = A.Fake<UserResponse>();
        var user = new User();

        A.CallTo(() => _userRepository.GetById(user.Id)).Returns(user);
        var userService = new UserService(_userRepository);
        //act
        var result = userService.PutChangeUser(request);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.Success);
    }

    //update user fail
    [Fact]
    public void UserServices_PutChangeUser_ReturnFail()
    {
        //Arrange
        var request = A.Fake<UserResponse>();
        var user = new User();

        A.CallTo(() => _userRepository.GetById(user.Id)).Returns(null);
        var userService = new UserService(_userRepository);
        //act
        var result = userService.PutChangeUser(request);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.InvalidInput);
    }



}
