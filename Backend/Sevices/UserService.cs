using Backend.Data;
using Backend.Entitise;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Sevices
{
    public class UserService(AppDbContext context) : IUserService
    {
        public async Task<int?> PutChangeUser(UserResponse request)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
                if (user == null)
                {
                    return 2;
                }
                user.Name = request.Name;
                user.Location = request.Location;
                user.Age = request.Age;
                user.Khoa = request.Khoa;
                user.Lop = request.Lop;
                user.sdt = request.sdt;
                if (request.urlUserImage != null)
                {
                    user.urlUserImage = request.urlUserImage;
                }
                await context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }


        }
    }
}
