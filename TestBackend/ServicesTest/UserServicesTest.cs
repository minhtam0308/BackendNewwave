using AutoMapper;
using Azure.Core;
using Backend.Common;
using Backend.Services;
using Backend.Sevices;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using FakeItEasy;
using FluentAssertions;
using System.Threading.Tasks;
namespace TestBackend.ServicesTest;


public class ImageServicesTest
{
    private readonly IImageRepository _imageRepository;

    public ImageServicesTest()
    {
        _imageRepository = A.Fake<IImageRepository>();
    }

    //update image success
    [Fact]
    public void ImageServices_UpdateImage_ReturnOK()
    {
        //Arrange
        var idUser = "id";
        var editImage = new BookImage() { Image = new byte[0] };

        A.CallTo(() => _imageRepository.GetById(editImage.Id)).Returns(editImage);
        var imageServices = new ImageServices(_imageRepository);
        //act
        var result = imageServices.UpdateImage(editImage, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.Success);
    }

    //update image fail
    [Fact]
    public void ImageServices_UpdateImage_ReturnFail()
    {
        //Arrange
        var idUser = "id";
        var editImage = new BookImage() { Image = new byte[0] };

        A.CallTo(() => _imageRepository.GetById(editImage.Id)).Returns(null);
        var imageServices = new ImageServices(_imageRepository);
        //act
        var result = imageServices.UpdateImage(editImage, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.InvalidInput);
    }

}
