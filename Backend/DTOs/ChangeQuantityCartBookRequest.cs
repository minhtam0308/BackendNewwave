namespace Backend.DTOs
{
    public class ChangeQuantityCartBookRequest
    {
        public required string IdCart {  get; set; }
        public required string IdBook {  get; set; }
        public int Quantity {  get; set; }
    }
}
