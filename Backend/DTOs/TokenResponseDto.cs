using Backend.DTOs;

namespace Backend.DTOs
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required UserResponse User { get; set; }

    }
}
