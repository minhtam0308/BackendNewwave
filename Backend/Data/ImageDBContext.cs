using Backend.Entitise;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ImageDBContext(DbContextOptions<ImageDBContext> options) : DbContext(options)
    {
        public DbSet<BookImage> BookImages { get; set; }
    }
}
