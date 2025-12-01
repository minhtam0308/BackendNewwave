using Backend.Entitise;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<MuonTra> MuonTras { get; set; }
        public DbSet<ChiTietMuon> ChiTietMuons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.IdAuthor);


            modelBuilder.Entity<ChiTietMuon>()
                .HasOne(b => b.MuonTras)
                .WithMany(b => b.ChiTietMuons)
                .HasForeignKey(b  => b.IdBook)
                .HasForeignKey(b => b.IdMuonTra);

            modelBuilder.Entity<MuonTra>()
                .HasOne(mt => mt.User)
                .WithMany(u => u.Users)
                .HasForeignKey(mt => mt.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MuonTra>()
                .HasOne(mt => mt.Admin)
                .WithMany(u => u.Admins)
                .HasForeignKey(mt => mt.IdAdmin)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
