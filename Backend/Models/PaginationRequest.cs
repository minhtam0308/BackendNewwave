namespace Backend.Models
{
    public class PaginationRequest
    {
        public int PageSize { get; set; } = 6;
        public int PageNumber { get; set; } = 1;
    }
}
