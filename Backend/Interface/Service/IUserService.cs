using Backend.Models;

namespace Backend.Interface.Service
{
    public interface IUserService
    {
        Task<int?> PutChangeUser(UserResponse request);

    }
}
