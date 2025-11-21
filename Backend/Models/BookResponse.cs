using Backend.Entitise;

namespace Backend.Models
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

        public byte[]? Image { get; set; }      // Ảnh bìa

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
