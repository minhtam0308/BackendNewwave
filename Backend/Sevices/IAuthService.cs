using Backend.Entitise;
using Backend.Models;

namespace Backend.Sevices
{
    public interface IAuthService
    {
        Task<User?> RegisterAsyn(UserDto request);
        Task<TokenResponseDto?> LoginAsyn(UserLoginDto request);
        Task<TokenResponseDto?> RefreshTokenAsyn(RefreshTokenRequest request);
    }
}
