namespace Backend.Entitise
{
    public class MuonTra
    {
        public Guid Id { get; set; }
        public List<ChiTietMuon> ChiTietMuons { get; set; } = new();
        public Guid IdAdmin { get; set; }
        public User? Admin { get; set; }
        public Guid IdUser { get; set; }
        public User? User { get; set; }
        public required DateTime hanDenMuon { get; set; }
        public DateTime? ngayMuon { get; set; }
        public DateTime? hanDenTra { get; set; }
        public DateTime? ngayTra { get; set; }
        public DateTime? ngayTao { get; set; } = DateTime.UtcNow;

    }
}
