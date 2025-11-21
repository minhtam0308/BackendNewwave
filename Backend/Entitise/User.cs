namespace Backend.Entitise
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }=String.Empty;
        public string Name { get; set; }=String.Empty;
        public string PasswordHash { get; set; } = String.Empty;
        public string Role { get; set; } = "user";
        public string? RefreshToken {  get; set; }
        public DateTime? RefreshTokeExpiryTime { get; set; }
    }
}
