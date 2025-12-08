namespace Backend.Models.Cart
{
    public class AddBookToCartRequest
    {
        public required string IdBook { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
