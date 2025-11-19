using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HanaHotel.DataAccessLayer.Concrete;
using HanaHotel.EntityLayer.Concrete;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public static class DataInitializer
{
    public static async Task TestDataAsync(IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

        if (context == null)
            return;

        // Apply migrations if any
        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();

        // About
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

        // Roles (simple Role entity)
        if (!context.Roles.Any())
        {
            var roles = new[]
            {
                new Role { Name = "Admin" },
                new Role { Name = "Manager" },
                new Role { Name = "Staff" },
                new Role { Name = "Customer" }
            };
            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }

        // Rooms
        if (!context.Rooms.Any())
        {
            var rooms = new List<Room>
            {
                new Room { RoomName = "Standard Room", Status = RoomStatus.Available, Description = "A cozy standard room with all essential amenities.", Size = 25, Price = 150, BedCount = "1" },
                new Room { RoomName = "Deluxe Room", Status = RoomStatus.Available, Description = "A spacious deluxe room with a beautiful view.", Size = 30, Price = 200, BedCount = "1" },
                new Room { RoomName = "Suite", Status = RoomStatus.Available, Description = "An elegant suite with a separate living area.", Size = 55, Price = 250, BedCount = "2" },
                new Room { RoomName = "Family Room", Status = RoomStatus.Available, Description = "A large family room with multiple beds.", Size = 40, Price = 300, BedCount = "3" },
                new Room { RoomName = "Business Room", Status = RoomStatus.Available, Description = "A comfortable room equipped for business travelers.", Size = 28, Price = 180, BedCount = "1" },
                new Room { RoomName = "Luxury Room", Status = RoomStatus.Available, Description = "A luxurious room with high-end furnishings.", Size = 35, Price = 220, BedCount = "1" },
                new Room { RoomName = "Penthouse", Status = RoomStatus.Available, Description = "A stunning penthouse suite with panoramic views.", Size = 120, Price = 750, BedCount = "2" }
            };
            context.Rooms.AddRange(rooms);
            await context.SaveChangesAsync();
        }

        // Services
        if (!context.Services.Any())
        {
            var services = new List<Service>
            {
                new Service { ServiceName = "Free Wi-Fi", Price = 0, Unit = "stay", Description = "Complimentary high-speed internet access throughout the property.", ServiceIcon = "fa fa-wifi" },
                new Service { ServiceName = "Fitness Center", Price = 0, Unit = "use", Description = "Access to fully-equipped fitness center.", ServiceIcon = "fa fa-dumbbell" },
                new Service { ServiceName = "Swimming Pool", Price = 0, Unit = "use", Description = "Outdoor swimming pool access.", ServiceIcon = "fa fa-swimmer" },
                new Service { ServiceName = "Spa Services", Price = 0, Unit = "service", Description = "Luxurious spa treatments available.", ServiceIcon = "fa fa-spa" },
                new Service { ServiceName = "Room Service", Price = 0, Unit = "order", Description = "Delicious meals delivered to your room.", ServiceIcon = "fa fa-utensils" },
                new Service { ServiceName = "Airport Shuttle", Price = 30, Unit = "trip", Description = "Convenient shuttle service to/from airport.", ServiceIcon = "fa fa-shuttle-van" }
            };
            context.Services.AddRange(services);
            await context.SaveChangesAsync();
        }

        // Message categories
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

        // Contacts
        if (!context.Contacts.Any())
        {
            context.Contacts.AddRange(
                new Contact { Name = "Alice Johnson", Email = "alice.johnson@example.com", Subject = "Inquiry about room availability", Message = "I would like to know if you have any rooms available for the weekend.", Date = DateTime.Now.AddDays(-2), MessageCategoryId = context.MessageCategories.First().MessageCategoryId },
                new Contact { Name = "Bob Smith", Email = "bob.smith@example.com", Subject = "Feedback on my stay", Message = "I had a great experience during my last stay. Thank you!", Date = DateTime.Now.AddDays(-5), MessageCategoryId = context.MessageCategories.Skip(1).First().MessageCategoryId }
            );
            await context.SaveChangesAsync();
        }

        // Guests (simple sample data)
        if (!context.Guests.Any())
        {
            context.Guests.AddRange(
                new Guest { Name = "John", Surname = "Doe", City = "New York" },
                new Guest { Name = "Jane", Surname = "Smith", City = "Los Angeles" },
                new Guest { Name = "Michael", Surname = "Johnson", City = "Chicago" }
            );
            await context.SaveChangesAsync();
        }

        // Users - use UserManager when available to create identity users
        List<User> createdUsers = new();
        if (userManager != null && !context.Users.Any())
        {
            // find role ids if needed (Role entity is a simple table)
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            var customerRole = context.Roles.FirstOrDefault(r => r.Name == "Customer");
            var managerRole = context.Roles.FirstOrDefault(r => r.Name == "Manager");

            var usersToCreate = new[]
            {
                new User { Name = "Admin User", DateOfBirth = new DateTime(1990,1,1), Gender = GenderType.Male, Address = "HQ", Phone = "1234567890", Email = "admin@example.com", UserName = "admin", RoleId = adminRole?.Id ?? 0 },
                new User { Name = "Elif Kaya", DateOfBirth = new DateTime(1992,5,3), Gender = GenderType.Female, Address = "Istanbul", Phone = "2345678901", Email = "elif.kaya@example.com", UserName = "elifkaya", RoleId = customerRole?.Id ?? 0 },
                new User { Name = "Murat Yilmaz", DateOfBirth = new DateTime(1988,3,15), Gender = GenderType.Male, Address = "Izmir", Phone = "3456789012", Email = "murat.yilmaz@example.com", UserName = "muratylmz", RoleId = managerRole?.Id ?? 0 },
                new User { Name = "Ayse Demir", DateOfBirth = new DateTime(1995,7,20), Gender = GenderType.Female, Address = "Bursa", Phone = "4567890123", Email = "ayse.demir@example.com", UserName = "aysedemir", RoleId = customerRole?.Id ?? 0 }
            };

            foreach (var u in usersToCreate)
            {
                var createResult = await userManager.CreateAsync(u, "Password123.");
                if (createResult.Succeeded)
                {
                    createdUsers.Add(u);
                }
                else
                {
                    // If creation failed, still attempt to add minimal record in context to keep DB consistent
                    if (!context.Users.Any(x => x.UserName == u.UserName))
                    {
                        context.Users.Add(u);
                        createdUsers.Add(u);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
        else if (context.Users.Any())
        {
            // if users exist, collect them
            createdUsers = context.Users.Take(4).ToList();
        }

        // Reviews (ensure there are rooms and users)
        if (!context.Reviews.Any() && context.Rooms.Any() && context.Users.Any())
        {
            var roomIds = context.Rooms.Select(r => r.Id).Take(6).ToList();
            var userIds = context.Users.Select(u => u.Id).Take(6).ToList();

            var reviews = new List<Review>();
            for (int i = 0; i < Math.Min(roomIds.Count, userIds.Count); i++)
            {
                reviews.Add(new Review
                {
                    UserId = userIds[i],
                    RoomId = roomIds[i],
                    Content = $"Sample review {i + 1} - excellent stay.",
                    RatingStars = 5
                });
            }

            if (reviews.Any())
            {
                context.Reviews.AddRange(reviews);
                await context.SaveChangesAsync();
            }
        }

        // Bookings - create some sample bookings linking to existing users and rooms
        if (!context.Bookings.Any() && context.Rooms.Any() && context.Users.Any())
        {
            var firstRoomId = context.Rooms.Select(r => r.Id).First();
            var firstUserId = context.Users.Select(u => u.Id).First();

            context.Bookings.AddRange(
                new Booking
                {
                    FullName = "John Doe",
                    Email = "john.doe@example.com",
                    Phone = "123-456-7890",
                    BookingDate = DateTime.Now.AddDays(-10),
                    CheckInDate = DateTime.Now.AddDays(7),
                    CheckOutDate = DateTime.Now.AddDays(10),
                    Status = BookingStatus.Confirmed,
                    AdditionalRequest = "Late check-in, ocean view",
                    RoomId = firstRoomId,
                    UserId = firstUserId
                },
                new Booking
                {
                    FullName = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Phone = "234-567-8901",
                    BookingDate = DateTime.Now.AddDays(-8),
                    CheckInDate = DateTime.Now.AddDays(10),
                    CheckOutDate = DateTime.Now.AddDays(14),
                    Status = BookingStatus.Pending,
                    AdditionalRequest = "Extra pillows",
                    RoomId = context.Rooms.Skip(1).Select(r => r.Id).FirstOrDefault(),
                    UserId = context.Users.Skip(1).Select(u => u.Id).FirstOrDefault()
                }
            );

            await context.SaveChangesAsync();
        }
    }
}