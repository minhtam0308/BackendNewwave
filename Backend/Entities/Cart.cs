namespace Backend.Entities
{
    public class Cart : BaseEntity
    {
        public Guid IdUser { get; set; }
        public User User { get; set; } = new();
        public List<CartBook> CartBooks { get; set; } = new List<CartBook>();
    }
}
