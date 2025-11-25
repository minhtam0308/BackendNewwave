using System.ComponentModel.DataAnnotations;

namespace Backend.Entitise
{
    public class Book
    {
        public Guid Id {  get; set; }
        public required string Title { get; set; }
        // Thêm khóa phụ kiểu Guid
        public Guid IdAuthor { get; set; }

        public Author? Author { get; set; }

        public string? Description { get; set; }

        public int TotalCopies { get; set; }       // Tổng số sách
        public int AvailableCopies { get; set; }   // Số sách còn lại

        public string? UrlBook { get; set; }      // Ảnh bìa

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
