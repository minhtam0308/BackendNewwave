using System.ComponentModel.DataAnnotations;

namespace Backend.Entitise
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        public string? NameAuthor { get; set; }
        public List<Book> Books { get; set; } = new();
    }
}
