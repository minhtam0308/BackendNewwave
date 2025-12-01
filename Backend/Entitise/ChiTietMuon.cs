namespace Backend.Entitise
{
    public class ChiTietMuon
    {
        public Guid Id { get; set; }
        public Guid IdMuonTra { get; set; }
        public MuonTra? MuonTras { get; set; }
        public Guid IdBook { get; set; }

    }
}
