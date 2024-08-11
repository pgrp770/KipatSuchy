namespace KipatSuchy.Models
{
    public class Response
    {
        public int Id { get; set; }
        public int ThreatId { get; set; }
        public Threat Threat { get; set; }
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}
