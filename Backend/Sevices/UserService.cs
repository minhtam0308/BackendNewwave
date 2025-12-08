using Backend.Data;
using Backend.Entities;
using Backend.Interface.Service;
using Backend.Models.Auth;
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
                user.Department = request.Department;
                user.Class = request.Class;
                user.PhoneNumber = request.PhoneNumber;
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
