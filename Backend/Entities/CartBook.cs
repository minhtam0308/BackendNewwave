namespace Backend.Entities
{
    public class CartBook: BaseEntity
    {
        public Guid IdCard { get; set; }
        public Cart? Cart { get; set; }
        public Guid IdBook { get; set; }

    }
}
