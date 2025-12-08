namespace Backend.Models.Author
{
    public class AuthorRenameRequest
    {
        public Guid Id { get; set; }
        public string? NameAuthor { get; set; }
    }
}
