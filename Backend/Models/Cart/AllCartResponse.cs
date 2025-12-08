using Backend.Models.Book;

namespace Backend.Models.Cart
{
    public class AllCartResponse
    {
        public Guid? IdCart {  get; set; }
        public List<BookResponse> ListBook { get; set; } = new List<BookResponse>();
    }
}
