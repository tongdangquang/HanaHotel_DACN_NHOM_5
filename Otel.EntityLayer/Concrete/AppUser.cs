using Microsoft.AspNetCore.Identity;

namespace Otel.EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string City { get; set; } = null!;
        public string Department { get; set; } = null!;

        public int WorkLocationId { get; set; }
        public WorkLocation? WorkLocation { get; set; }

    }

}