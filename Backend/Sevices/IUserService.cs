using Backend.Models;

namespace Backend.Sevices
{
    public interface IUserService
    {
        Task<int?> PutChangeUser(UserResponse request);

    }
}
