using Backend.Interface.IRepositories;
using BeNewNewave.Data;
using BeNewNewave.Entities;
using BeNewNewave.Repositories;

namespace Backend.Repositories
{
    public class DetailBorrowRepo : BaseRepository<DetailBorrow>, IDetailBorrowRepository
    {
        public DetailBorrowRepo(AppDbContext context) : base(context)
        {
        }
    }
}
