using AutoMapper;
using Backend.Common;
using Backend.Sevices;
using Backend.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using FakeItEasy;
using FluentAssertions;
namespace TestBackend.ServicesTest;


public class AuthorServicesTest
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorServicesTest()
    {
        _authorRepository = A.Fake<IAuthorRepository>();
        _mapper = A.Fake<IMapper>();
    }

    [Fact]
    public void AuthorServices_EditAuthor_ReturnOK()
    {
        //Arrange
        var author = new AuthorRenameRequest() { NameAuthor = "abc"};
        var authorEdit = new Author();
        string idUser = "";

        A.CallTo(() => _authorRepository.GetById(author.Id)).Returns(authorEdit);
        var authorService = new AuthorServices(_authorRepository, _mapper);
        //act
        var result = authorService.EditAuthor(author, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.Success);
    }

    [Fact]
    public void AuthorServices_EditAuthor_AuthorEditNull()
    {
        //Arrange
        var author = new AuthorRenameRequest() { NameAuthor = "abc" };
        var authorEdit = new Author();
        string idUser = "";

        A.CallTo(() => _authorRepository.GetById(author.Id)).Returns(null);
        var authorService = new AuthorServices(_authorRepository, _mapper);
        //act
        var result = authorService.EditAuthor(author, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.InvalidInput);
    }


    [Fact]
    public void AuthorServices_DeleteAuthor_ReturnOK()
    {
        //Arrange
        var oldAuthor = new Author();
        string idUser = "";

        A.CallTo(() => _authorRepository.GetById(oldAuthor.Id)).Returns(oldAuthor);
        var authorService = new AuthorServices(_authorRepository, _mapper);
        //act
        var result = authorService.DeleteAuthor(oldAuthor.Id, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.Success);
    }

    [Fact]
    public void AuthorServices_DeleteAuthor_OldAuthorNull()
    {
        //Arrange
        var oldAuthor = new Author();
        string idUser = "";

        A.CallTo(() => _authorRepository.GetById(oldAuthor.Id)).Returns(null);
        var authorService = new AuthorServices(_authorRepository, _mapper);
        //act
        var result = authorService.DeleteAuthor(oldAuthor.Id, idUser);

        //Assert
        result.Should().NotBeNull();
        result.errorCode.Should().Be(ErrorCode.InvalidInput);
    }
}
