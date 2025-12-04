namespace Backend.Entities
{
    public class Cart : BaseEntity
    {
        public Guid idUser { get; set; }
        public List<CartBook>? cartBooks { get; set; }
    }
}
