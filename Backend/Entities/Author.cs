using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    public class Author : BaseEntity
    {
        public string? NameAuthor { get; set; }
        public List<Book> Books { get; set; } = new();
    }
}
