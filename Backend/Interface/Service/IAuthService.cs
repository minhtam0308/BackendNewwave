using Backend.Entities;
using Backend.Models;

namespace Backend.Interface.Service
{
    public interface IAuthService
    {
        Task<User?> RegisterAsyn(UserDto request);
        Task<TokenResponseDto?> LoginAsyn(UserLoginDto request);
        Task<TokenResponseDto?> RefreshTokenAsyn(RefreshTokenRequest request);
    }
}
