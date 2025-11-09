using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.DataAccessLayer.Concrete
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<MessageCategory> MessageCategories { get; set; }
        public DbSet<WorkLocation> WorkLocations { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=db31658.public.databaseasp.net; Database=db31658; User Id=db31658; Password=12345678; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";
			optionsBuilder.UseSqlServer(connectionString);
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.MessageCategory)
                .WithMany(cat => cat.Contacts)
                .HasForeignKey(c => c.MessageCategoryId);

            modelBuilder.Entity<AppUser>()
                .HasOne(c => c.WorkLocation)
                .WithMany(c => c.Users)
                .HasForeignKey(c => c.WorkLocationId);
        }
    }
}
