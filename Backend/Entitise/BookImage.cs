namespace Backend.Entitise
{
    public class BookImage
    {
        public Guid Id { get; set; }
        public required byte[] image { get; set; }
    }
}
