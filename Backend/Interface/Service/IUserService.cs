using Backend.Models.Auth;

namespace Backend.Interface.Service
{
    public interface IUserService
    {
        Task<int?> PutChangeUser(UserResponse request);

    }
}
