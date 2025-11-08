using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        // Convenience properties for application-level role access.
        // These are not mapped to the Identity tables (roles are stored in AspNetRoles/AspNetUserRoles).
        [NotMapped]
        public IList<string> Roles { get; set; } = new List<string>();

        [NotMapped]
        public string? PrimaryRole => Roles.Count > 0 ? Roles[0] : null;
    }

}