using Backend.Entities;
using Backend.Models.Auth;

namespace Backend.Interface.Service
{
    public interface IAuthService
    {
        Task<int?> RegisterAsyn(UserDto request);
        Task<TokenResponseDto?> LoginAsyn(UserLoginDto request);
        Task<TokenResponseDto?> RefreshTokenAsyn(RefreshTokenRequest request);
    }
}
