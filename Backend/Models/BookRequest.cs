namespace Backend.Models
{
    public class BookRequest
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public Guid IdAuthor { get; set; }
        public int TotalCopies { get; set; }       
        public byte[]? Image { get; set; }
    }
}
