using AutoMapper;
using Backend.Entities;
using Backend.Models.Book;

namespace Backend.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Book, BookResponse>();
        }
    }
}
