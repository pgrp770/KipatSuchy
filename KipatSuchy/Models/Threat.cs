namespace KipatSuchy.Models
{
    public class Threat
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Weapon { get; set; }
        public string Origin { get; set; }
        public bool IsActive { get; set; } = false;
        public bool puzaz { get; set; } = false;
        public DateTime? LaunchTime { get; set; }
      
 
    }
}
