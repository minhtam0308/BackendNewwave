using AutoMapper;
using Azure.Core;
using Backend.Common;
using Backend.Services;
using Backend.Sevices;
using Backend.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using FakeItEasy;
using FluentAssertions;
using System.Threading.Tasks;
namespace TestBackend.ServicesTest;


public class CartBookServicesTest
{
    private readonly ICartBookRepository _cartBookRepository;
    private readonly IBookRepository _bookRepository;

    public CartBookServicesTest()
    {
        _cartBookRepository = A.Fake<ICartBookRepository>();
        _bookRepository = A.Fake<IBookRepository>();
    }

    //delete book from cart success
    [Fact]
    public void CartBookServices_DeleteCartBook_ReturnOK()
    {
        //Arrange
        var idUser = "id";
        var cartBook = new CartBook();

        A.CallTo(() => _cartBookRepository.GetByIdBookAndIdCart(cartBook.IdCart, cartBook.IdBook)).Returns(cartBook);
        var cartBookookServices = new CartBookServices(_cartBookRepository, _bookRepository);
        //act
        var result = cartBookookServices.DeleteCartBook(cartBook.IdCart, cartBook.IdBook, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.Success);
    }

    //delete book from cart fail
    [Fact]
    public void CartBookServices_DeleteCartBook_ReturnFail()
    {
        //Arrange

        var idUser = "id";
        var cartBook = new CartBook();

        A.CallTo(() => _cartBookRepository.GetByIdBookAndIdCart(cartBook.IdCart, cartBook.IdBook)).Returns(null);
        var cartBookookServices = new CartBookServices(_cartBookRepository, _bookRepository);
        //act
        var result = cartBookookServices.DeleteCartBook(cartBook.IdCart, cartBook.IdBook, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.InvalidInput);
    }

    //put book from cart success
    [Fact]
    public void CartBookServices_PutQuantityCartBook_ReturnOk()
    {
        //Arrange
        var quantity = 1;
        var idUser = "id";
        var cartBook = new CartBook();
        var book = new Book() { Title = "Test", AvailableCopies = 10};

        A.CallTo(() => _cartBookRepository.GetByIdBookAndIdCart(cartBook.IdCart, cartBook.IdBook)).Returns(cartBook);
        A.CallTo(() => _bookRepository.GetById(cartBook.IdBook)).Returns(book);
        var cartBookookServices = new CartBookServices(_cartBookRepository, _bookRepository);
        //act
        var result = cartBookookServices.PutQuantityCartBook(cartBook.IdCart, cartBook.IdBook, quantity, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.Success);
    }

    //put book from cart fail cartBook not exist
    [Fact]
    public void CartBookServices_PutQuantityCartBook_ReturnCartBookNotExist()
    {
        //Arrange
        var quantity = 1;
        var idUser = "id";
        var cartBook = new CartBook();
        var book = new Book() { Title = "Test", AvailableCopies = 10 };

        A.CallTo(() => _cartBookRepository.GetByIdBookAndIdCart(cartBook.IdCart, cartBook.IdBook)).Returns(null);
        A.CallTo(() => _bookRepository.GetById(cartBook.IdBook)).Returns(book);
        var cartBookookServices = new CartBookServices(_cartBookRepository, _bookRepository);
        //act
        var result = cartBookookServices.PutQuantityCartBook(cartBook.IdCart, cartBook.IdBook, quantity, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.InvalidInput);
    }

    //put book from cart fail book available smaller quantity
    [Fact]
    public void CartBookServices_PutQuantityCartBook_ReturnAvailableSmallerQuantity()
    {
        //Arrange
        var quantity = 10;
        var idUser = "id";
        var cartBook = new CartBook();
        var book = new Book() { Title = "Test", AvailableCopies = 1 };

        A.CallTo(() => _cartBookRepository.GetByIdBookAndIdCart(cartBook.IdCart, cartBook.IdBook)).Returns(cartBook);
        A.CallTo(() => _bookRepository.GetById(cartBook.IdBook)).Returns(book);
        var cartBookookServices = new CartBookServices(_cartBookRepository, _bookRepository);
        //act
        var result = cartBookookServices.PutQuantityCartBook(cartBook.IdCart, cartBook.IdBook, quantity, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.InvalidInput);
    }

}
