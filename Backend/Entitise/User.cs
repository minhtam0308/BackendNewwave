namespace Backend.Entitise
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }=String.Empty;
        public string Name { get; set; }=String.Empty;
        public int? Age { get; set; }
        public string? Location { get; set; }
        public string PasswordHash { get; set; } = String.Empty;
        public string Role { get; set; } = "user";
        public string? RefreshToken {  get; set; }
        public string? urlUserImage { get; set; } = "258d5e1a-ff57-4092-2a5d-08de2e43c05d";
        public DateTime? RefreshTokeExpiryTime { get; set; }
    }
}
