using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HanaHotel.DataAccessLayer.Concrete;
using HanaHotel.EntityLayer.Concrete;

public static class DataInitializer
{
    public static async Task TestDataAsync(IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        if (context != null)
        {
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

            if (!context.Abouts.Any())
            {
                context.Abouts.Add(new About
                {
                    Title1 = "Welcome to Our Luxury Resort",
                    Title2 = "Experience Unparalleled Comfort and Service",
                    Content = "Nestled in the heart of the city, our luxury resort offers world-class amenities, exceptional service, and a serene atmosphere. Whether you're here for business or leisure, we promise a memorable stay with our state-of-the-art facilities and warm hospitality.",
                    RoomCount = 250,
                    StaffCount = 150,
                    CustomerCount = 5000
                });
                await context.SaveChangesAsync();
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new AppRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new AppRole
                    {
                        Name = "Manager",
                        NormalizedName = "MANAGER",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new AppRole
                    {
                        Name = "Staff",
                        NormalizedName = "STAFF",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new AppRole
                    {
                        Name = "Customer",
                        NormalizedName = "CUSTOMER",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Bookings.Any())
            {
                context.Bookings.AddRange(
                    new Booking
                    {
                        Name = "John Doe",
                        Email = "john.doe@example.com",
                        CheckInDate = new DateTime(2024, 10, 10),
                        CheckOutDate = new DateTime(2024, 10, 15),
                        AdultCount = 2,
                        ChildCount = 1,
                        Room = "101",
                        SpecialRequest = "Late check-in, ocean view",
                        Status = BookingStatus.Confirmed
                    },
                    new Booking
                    {
                        Name = "Jane Smith",
                        Email = "jane.smith@example.com",
                        CheckInDate = new DateTime(2024, 10, 12),
                        CheckOutDate = new DateTime(2024, 10, 20),
                        AdultCount = 3,
                        ChildCount = 2,
                        Room = "205",
                        SpecialRequest = "Extra pillows",
                        Status = BookingStatus.Pending
                    },
                    new Booking
                    {
                        Name = "Michael Johnson",
                        Email = "michael.johnson@example.com",
                        CheckInDate = new DateTime(2024, 10, 5),
                        CheckOutDate = new DateTime(2024, 10, 10),
                        AdultCount = 1,
                        ChildCount = 0,
                        Room = "302",
                        SpecialRequest = "High floor",
                        Status = BookingStatus.Cancelled
                    },
                    new Booking
                    {
                        Name = "Emily Davis",
                        Email = "emily.davis@example.com",
                        CheckInDate = new DateTime(2024, 10, 8),
                        CheckOutDate = new DateTime(2024, 10, 12),
                        AdultCount = 2,
                        ChildCount = 0,
                        Room = "201",
                        SpecialRequest = "No specific requests",
                        Status = BookingStatus.Confirmed
                    },
                    new Booking
                    {
                        Name = "David Wilson",
                        Email = "david.wilson@example.com",
                        CheckInDate = new DateTime(2024, 10, 15),
                        CheckOutDate = new DateTime(2024, 10, 20),
                        AdultCount = 2,
                        ChildCount = 1,
                        Room = "105",
                        SpecialRequest = "Adjacent rooms",
                        Status = BookingStatus.Completed
                    },
                    new Booking
                    {
                        Name = "Olivia Brown",
                        Email = "olivia.brown@example.com",
                        CheckInDate = new DateTime(2024, 10, 1),
                        CheckOutDate = new DateTime(2024, 10, 5),
                        AdultCount = 1,
                        ChildCount = 0,
                        Room = "107",
                        SpecialRequest = "Vegetarian meals",
                        Status = BookingStatus.Confirmed
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.MessageCategories.Any())
            {
                context.MessageCategories.AddRange(
                    new MessageCategory { MessageCategoryName = "General Inquiry" },
                    new MessageCategory { MessageCategoryName = "Booking Request" },
                    new MessageCategory { MessageCategoryName = "Feedback" },
                    new MessageCategory { MessageCategoryName = "Complaint" },
                    new MessageCategory { MessageCategoryName = "Service Request" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Contacts.Any())
            {
                context.Contacts.AddRange(
                    new Contact
                    {
                        Name = "Alice Johnson",
                        Email = "alice.johnson@example.com",
                        Subject = "Inquiry about room availability",
                        Message = "I would like to know if you have any rooms available for the weekend.",
                        Date = DateTime.Now.AddDays(-2),
                        MessageCategoryId = 1
                    },
                    new Contact
                    {
                        Name = "Bob Smith",
                        Email = "bob.smith@example.com",
                        Subject = "Feedback on my stay",
                        Message = "I had a great experience during my last stay. Thank you!",
                        Date = DateTime.Now.AddDays(-5),
                        MessageCategoryId = 2
                    },
                    new Contact
                    {
                        Name = "Charlie Brown",
                        Email = "charlie.brown@example.com",
                        Subject = "Cancellation request",
                        Message = "I need to cancel my reservation for next week.",
                        Date = DateTime.Now.AddDays(-1),
                        MessageCategoryId = 3
                    },
                    new Contact
                    {
                        Name = "Diana Prince",
                        Email = "diana.prince@example.com",
                        Subject = "Room service inquiry",
                        Message = "What are the available options for room service?",
                        Date = DateTime.Now.AddDays(-3),
                        MessageCategoryId = 1
                    },
                    new Contact
                    {
                        Name = "Ethan Hunt",
                        Email = "ethan.hunt@example.com",
                        Subject = "Lost item",
                        Message = "I left my jacket in the room after checkout. Can you help me?",
                        Date = DateTime.Now.AddDays(-4),
                        MessageCategoryId = 4
                    },
                    new Contact
                    {
                        Name = "Fiona Gallagher",
                        Email = "fiona.gallagher@example.com",
                        Subject = "Request for a special arrangement",
                        Message = "I would like to request a birthday surprise for my partner during our stay.",
                        Date = DateTime.Now.AddDays(-1),
                        MessageCategoryId = 5
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Guests.Any())
            {
                context.Guests.AddRange(
                    new Guest { Name = "John", Surname = "Doe", City = "New York" },
                    new Guest { Name = "Jane", Surname = "Smith", City = "Los Angeles" },
                    new Guest { Name = "Michael", Surname = "Johnson", City = "Chicago" },
                    new Guest { Name = "Emily", Surname = "Davis", City = "Houston" },
                    new Guest { Name = "James", Surname = "Wilson", City = "Phoenix" },
                    new Guest { Name = "Mary", Surname = "Garcia", City = "Philadelphia" },
                    new Guest { Name = "David", Surname = "Martinez", City = "San Antonio" },
                    new Guest { Name = "Sarah", Surname = "Lopez", City = "San Diego" },
                    new Guest { Name = "Daniel", Surname = "Hernandez", City = "Dallas" },
                    new Guest { Name = "Laura", Surname = "Gonzalez", City = "San Jose" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Rooms.Any())
            {
                context.Rooms.AddRange(
                    new Room { RoomNumber = "101", RoomCoverImage = "room-1.jpg", Price = 150, Title = "Standard Room", BedCount = "1", BathCount = "1", Wifi = "true", Description = "A cozy standard room with all essential amenities." },
                    new Room { RoomNumber = "102", RoomCoverImage = "room-2.jpg", Price = 200, Title = "Deluxe Room", BedCount = "1", BathCount = "1", Wifi = "true", Description = "A spacious deluxe room with a beautiful view." },
                    new Room { RoomNumber = "201", RoomCoverImage = "room-3.jpg", Price = 250, Title = "Suite", BedCount = "2", BathCount = "2", Wifi = "true", Description = "An elegant suite with a separate living area." },
                    new Room { RoomNumber = "202", RoomCoverImage = "room-1.jpg", Price = 300, Title = "Family Room", BedCount = "3", BathCount = "1", Wifi = "true", Description = "A large family room with multiple beds." },
                    new Room { RoomNumber = "301", RoomCoverImage = "room-2.jpg", Price = 180, Title = "Business Room", BedCount = "1", BathCount = "1", Wifi = "true", Description = "A comfortable room equipped for business travelers." },
                    new Room { RoomNumber = "302", RoomCoverImage = "room-3.jpg", Price = 220, Title = "Luxury Room", BedCount = "1", BathCount = "1", Wifi = "true", Description = "A luxurious room with high-end furnishings." },
                    new Room { RoomNumber = "401", RoomCoverImage = "room-1.jpg", Price = 270, Title = "Penthouse", BedCount = "2", BathCount = "2", Wifi = "true", Description = "A stunning penthouse suite with panoramic views." }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Services.Any())
            {
                context.Services.AddRange(
                    new Service { ServiceIcon = "fa fa-wifi fa-2x text-primary", Title = "Free Wi-Fi", Description = "Enjoy complimentary high-speed internet access throughout the property." },
                    new Service { ServiceIcon = "fa fa-bicycle fa-2x text-primary", Title = "Fitness Center", Description = "Stay fit and healthy with our fully-equipped fitness center." },
                    new Service { ServiceIcon = "fa fa-swimming-pool fa-2x text-primary", Title = "Swimming Pool", Description = "Take a refreshing dip in our outdoor swimming pool." },
                    new Service { ServiceIcon = "fa fa-spa fa-2x text-primary", Title = "Spa Services", Description = "Relax and rejuvenate with our luxurious spa treatments." },
                    new Service { ServiceIcon = "fa fa-utensils fa-2x text-primary", Title = "Room Service", Description = "Enjoy delicious meals delivered right to your room." },
                    new Service { ServiceIcon = "fa fa-car fa-2x text-primary", Title = "Car Rental", Description = "Rent a car to explore the city at your convenience." },
                    new Service { ServiceIcon = "fa fa-umbrella-beach fa-2x text-primary", Title = "Beach Access", Description = "Relax at our private beach area with lounge chairs." },
                    new Service { ServiceIcon = "fa fa-concierge-bell fa-2x text-primary", Title = "Concierge Service", Description = "Our concierge is here to assist you with any requests." },
                    new Service { ServiceIcon = "fa fa-parking fa-2x text-primary", Title = "Parking", Description = "Complimentary parking available for all guests." },
                    new Service { ServiceIcon = "fa fa-coffee fa-2x text-primary", Title = "Coffee Shop", Description = "Enjoy a selection of beverages and snacks at our coffee shop." },
                    new Service { ServiceIcon = "fa fa-child fa-2x text-primary", Title = "Kids Club", Description = "Fun activities for children in a safe environment." },
                    new Service { ServiceIcon = "fa fa-shuttle-van fa-2x text-primary", Title = "Airport Shuttle", Description = "Convenient shuttle service to and from the airport." }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Staffs.Any())
            {
                context.Staffs.AddRange(
                    new Staff { Name = "John Doe", Title = "General Manager", Link1 = "team-1.jpg", Link2 = "https://www.linkedin.com/in/johndoe", Link3 = "https://www.twitter.com/johndoe" },
                    new Staff { Name = "Jane Smith", Title = "Front Desk Manager", Link1 = "team-2.jpg", Link2 = "https://www.linkedin.com/in/janesmith", Link3 = "https://www.twitter.com/janesmith" },
                    new Staff { Name = "Michael Brown", Title = "Head Chef", Link1 = "team-3.jpg", Link2 = "https://www.linkedin.com/in/michaelbrown", Link3 = "https://www.twitter.com/michaelbrown" },
                    new Staff { Name = "Emily Davis", Title = "Housekeeping Manager", Link1 = "team-4.jpg", Link2 = "https://www.linkedin.com/in/emilydavis", Link3 = "https://www.twitter.com/emilydavis" },
                    new Staff { Name = "Chris Johnson", Title = "Sales Manager", Link1 = "team-1.jpg", Link2 = "https://www.linkedin.com/in/chrisjohnson", Link3 = "https://www.twitter.com/chrisjohnson" },
                    new Staff { Name = "Sarah Wilson", Title = "Marketing Specialist", Link1 = "team-2.jpg", Link2 = "https://www.linkedin.com/in/sarahwilson", Link3 = "https://www.twitter.com/sarahwilson" },
                    new Staff { Name = "David Lee", Title = "Maintenance Supervisor", Link1 = "team-3.jpg", Link2 = "https://www.linkedin.com/in/davidlee", Link3 = "https://www.twitter.com/davidlee" },
                    new Staff { Name = "Laura Martinez", Title = "Event Coordinator", Link1 = "team-4.jpg", Link2 = "https://www.linkedin.com/in/lauramartinez", Link3 = "https://www.twitter.com/lauramartinez" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Testimonials.Any())
            {
                context.Testimonials.AddRange(
                    new Testimonial { Name = "Alice Johnson", Title = "Happy Customer", Description = "This resort exceeded my expectations! The staff was incredible, and the amenities were top-notch.", Image = "testimonial-1.jpg" },
                    new Testimonial { Name = "Robert Smith", Title = "Business Traveler", Description = "A perfect place for business meetings. The service is exceptional, and the environment is very professional.", Image = "testimonial-2.jpg" },
                    new Testimonial { Name = "Emma Brown", Title = "Leisure Traveler", Description = "I had a fantastic time here! The spa services were relaxing, and the pool was beautiful.", Image = "testimonial-3.jpg" },
                    new Testimonial { Name = "James Wilson", Title = "Family Vacationer", Description = "Our family loved every moment. There were activities for everyone, and the rooms were spacious.", Image = "testimonial-4.jpg" },
                    new Testimonial { Name = "Sophia Taylor", Title = "Wedding Guest", Description = "Attending a wedding here was magical! The location was stunning, and the staff catered to every need.", Image = "testimonial-1.jpg" },
                    new Testimonial { Name = "Michael Clark", Title = "Frequent Visitor", Description = "I come here every year! Consistently great service and beautiful surroundings.", Image = "testimonial-2.jpg" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.WorkLocations.Any())
            {
                context.WorkLocations.AddRange(
                    new WorkLocation { WorkLocationCityName = "New York", WorkLocationCountry = "USA" },
                    new WorkLocation { WorkLocationCityName = "London", WorkLocationCountry = "UK" },
                    new WorkLocation { WorkLocationCityName = "Tokyo", WorkLocationCountry = "Japan" },
                    new WorkLocation { WorkLocationCityName = "Paris", WorkLocationCountry = "France" },
                    new WorkLocation { WorkLocationCityName = "Berlin", WorkLocationCountry = "Germany" },
                    new WorkLocation { WorkLocationCityName = "Sydney", WorkLocationCountry = "Australia" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                AppUser appUser = new AppUser
                {
                    Name = "Admin",
                    LastName = "Admin",
                    UserName = "admin",
                    Email = "admin@example.com",
                    PhoneNumber = "123-456-7890",
                    City = "Ankara",
                    Department = "IT",
                    WorkLocationId = 1,
                };
                AppUser appUser2 = new AppUser
                {
                    Name = "Elif",
                    LastName = "Kaya",
                    UserName = "elifkaya",
                    Email = "elif.kaya@example.com",
                    PhoneNumber = "234-567-8901",
                    City = "İstanbul",
                    Department = "Marketing",
                    WorkLocationId = 2,
                };
                AppUser appUser3 = new AppUser
                {
                    Name = "Murat",
                    LastName = "Yılmaz",
                    UserName = "muratylmz",
                    Email = "murat.yilmaz@example.com",
                    PhoneNumber = "345-678-9012",
                    City = "İzmir",
                    Department = "Finance",
                    WorkLocationId = 3,
                };
                AppUser appUser4 = new AppUser
                {
                    Name = "Ayşe",
                    LastName = "Demir",
                    UserName = "aysedemir",
                    Email = "ayse.demir@example.com",
                    PhoneNumber = "456-789-0123",
                    City = "Bursa",
                    Department = "HR",
                    WorkLocationId = 4,
                };

                await userManager.CreateAsync(appUser, "Admin123.");
                await userManager.CreateAsync(appUser2, "ElifKaya123.");
                await userManager.CreateAsync(appUser3, "MuratYilmaz123.");
                await userManager.CreateAsync(appUser4, "AyseDemir123.");

                await context.SaveChangesAsync();
            }
        }
    }
}