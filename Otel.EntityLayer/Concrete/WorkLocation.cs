
namespace Otel.EntityLayer.Concrete
{
    public class WorkLocation
    {
        public int WorkLocationId { get; set; }
        public string WorkLocationCityName { get; set; } = null!;
        public string WorkLocationCountry { get; set; } = null!;
        public ICollection<AppUser> Users { get; set; } = [];

    }
}