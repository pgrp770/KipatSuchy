using KipatSuchy.Models;

namespace KipatSuchy.ViewModels
{
    public class AddResponseVM
    {
        public List<Threat> Threats { get; set; } = [];
        public Threat Threat { get; set; }
        public List<Response> Responses { get; set; } = [];
        public Response Response { get; set; }
        public List<Manager> Managers { get; set; } = [];

    }
}
