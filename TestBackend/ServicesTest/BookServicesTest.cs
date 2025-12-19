using AutoMapper;
using Azure.Core;
using Backend.Common;
using Backend.Sevices;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using FakeItEasy;
using FluentAssertions;
using System.Threading.Tasks;
namespace TestBackend.ServicesTest;


public class BookServicesTest
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookServicesTest()
    {
        _bookRepository = A.Fake<IBookRepository>();
        _mapper = A.Fake<IMapper>();
    }

    //get all books success
    [Fact]
    public void BookServices_GetAllBook_ReturnOK()
    {
        //Arrange
        var fakeBooks = new List<BookResponse>
        {
            new BookResponse {Title = "Book 1"},
            new BookResponse {Title = "Book 2"}
        };

        A.CallTo(() => _bookRepository.GetAllBook()).Returns(fakeBooks);
        var bookServices = new BookServices(_bookRepository, _mapper);
        //act
        var result = bookServices.GetAllBook();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(fakeBooks);
    }

    //get all paginate books success
    [Fact]
    public async Task BookServices_GetBookPaginateAsync_ReturnOK()
    {
        //Arrange
        var paginationRequest = new PaginationRequest {PageNumber = 1, PageSize = 1};
        var totalCount = 3;
        var fakeBooks = new List<BookResponse>
        {
            new BookResponse {Title = "Book 1"},
            new BookResponse {Title = "Book 2"}
        };
        A.CallTo( () => _bookRepository.GetCountBook(paginationRequest)).Returns(Task.FromResult<int>(totalCount));
        A.CallTo( () => _bookRepository.GetBookPaginate(paginationRequest)).Returns(Task.FromResult<List<BookResponse>?>(fakeBooks));
        var bookServices = new BookServices(_bookRepository, _mapper);
        //act
        var result = await bookServices.GetBookPaginateAsync(paginationRequest);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.Success);
    }

    //get all paginate books fail
    [Fact]
    public async Task BookServices_GetBookPaginateAsync_ReturnFail()
    {
        //Arrange
        var paginationRequest = new PaginationRequest { PageNumber = 100, PageSize = 10 };
        var totalCount = 3;
        var fakeBooks = new List<BookResponse>
        {
            new BookResponse {Title = "Book 1"},
            new BookResponse {Title = "Book 2"}
        };
        A.CallTo(() => _bookRepository.GetCountBook(paginationRequest)).Returns(Task.FromResult<int>(totalCount));
        A.CallTo(() => _bookRepository.GetBookPaginate(paginationRequest)).Returns(Task.FromResult<List<BookResponse>?>(fakeBooks));
        var bookServices = new BookServices(_bookRepository, _mapper);
        //act
        var result = await bookServices.GetBookPaginateAsync(paginationRequest);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.InvalidInput);
    }

    //post books success
    [Fact]
    public async Task BookServices_PostCreateBook_ReturnOk()
    {
        //Arrange
        var request = new BookRequest() { Title = "Test"};
        var bookNew = new Book() { Title = "Test"};
        string idUser = "";
        A.CallTo(() => _mapper.Map<Book>(request)).Returns(bookNew);
        var bookServices = new BookServices(_bookRepository, _mapper);
        //act
        var result = bookServices.PostCreateBook(request, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.Success);
    }

    //put books success
    [Fact]
    public async Task BookServices_PutBook_ReturnOk()
    {
        //Arrange
        var bookEdit = new Book() { Title = "Test" };
        string idUser = "";
        A.CallTo(() => _bookRepository.GetById(bookEdit.Id)).Returns(bookEdit);
        var bookServices = new BookServices(_bookRepository, _mapper);

        //act
        var result = bookServices.PutBook(bookEdit, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.Success);
    }

    //put books fail
    [Fact]
    public async Task BookServices_PutBook_ReturnFail()
    {
        //Arrange
        var bookEdit = new Book() { Title = "Test" };
        string idUser = "";
        A.CallTo(() => _bookRepository.GetById(bookEdit.Id)).Returns(null);
        var bookServices = new BookServices(_bookRepository, _mapper);

        //act
        var result = bookServices.PutBook(bookEdit, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.InvalidInput);
    }
}
