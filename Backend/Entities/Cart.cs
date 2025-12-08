namespace Backend.Entities
{
    public class Cart : BaseEntity
    {
        public Guid idUser { get; set; }
        public User? User { get; set; }
        public List<CartBook> cartBooks { get; set; } = new List<CartBook>();
    }
}
