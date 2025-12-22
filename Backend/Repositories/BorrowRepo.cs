using Backend.Interface.IRepositories;
using BeNewNewave.Data;
using BeNewNewave.Entities;
using BeNewNewave.Repositories;

namespace Backend.Repositories
{
    public class BorrowRepo : BaseRepository<Borrow>, IBorrowRepository
    {
        public BorrowRepo(AppDbContext context) : base(context)
        {
        }
    }
}
