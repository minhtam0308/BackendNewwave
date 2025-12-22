using Backend.DTOs;

namespace Backend.DTOs
{
    public class BorrowBookRequest
    {
        public List<AddBookToCartRequest>? ListBooks { get; set; }
        public required DateTime ExpiresBorrow { get; set; }
        public required int NumberBorrowDate { get; set; }
    }
}
