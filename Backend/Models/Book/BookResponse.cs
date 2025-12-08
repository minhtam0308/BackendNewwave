using Backend.Entities;

namespace Backend.Models.Book
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        // Thêm khóa phụ kiểu Guid
        public string? NameAuthor { get; set; }
        public string? Description { get; set; }
        public Guid IdAuthor { get; set; }
        public int TotalCopies { get; set; }       // Tổng số sách
        public int AvailableCopies { get; set; }   // Số sách còn lại

        public string? UrlBook { get; set; }      // Ảnh bìa

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
