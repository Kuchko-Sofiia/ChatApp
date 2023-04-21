using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DAL.Extentions
{
    public static class ModelBuilderExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserName = "user1",
                    Email = "user1@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "+380734636585",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user2",
                    Email = "user2@example.com",
                    FirstName = "Jane",
                    LastName = "Doe",
                    PhoneNumber = "+380674111985",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user3",
                    Email = "user3@example.com",
                    FirstName = "Bob",
                    LastName = "Smith",
                    PhoneNumber = "+380664000685",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user4",
                    Email = "user4@example.com",
                    FirstName = "Alice",
                    LastName = "Johnson",
                    PhoneNumber = "+380668779985",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user5",
                    Email = "user5@example.com",
                    FirstName = "Mike",
                    LastName = "Davis",
                    PhoneNumber = "+380994679985",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user6",
                    Email = "user6@example.com",
                    FirstName = "Sarah",
                    LastName = "Wilson",
                    PhoneNumber = "+380994679985",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user7",
                    Email = "user7@example.com",
                    FirstName = "David",
                    LastName = "Anderson",
                    PhoneNumber = "+380674679985",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user8",
                    Email = "user8@example.com",
                    FirstName = "Karen",
                    LastName = "Thomas",
                    PhoneNumber = "+380634679985",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user9",
                    Email = "user9@example.com",
                    FirstName = "Tom",
                    LastName = "Jackson",
                    PhoneNumber = "+380734679985",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user10",
                    Email = "user10@example.com",
                    FirstName = "Lisa",
                    LastName = "Parker",
                    PhoneNumber = "+380634679555",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user11",
                    Email = "user11@example.com",
                    FirstName = "Sam",
                    LastName = "Taylor",
                    PhoneNumber = "+380674679985",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user12",
                    Email = "user12@example.com",
                    FirstName = "Megan",
                    LastName = "Harris",
                    PhoneNumber = "+380674635985",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user13",
                    Email = "user13@example.com",
                    FirstName = "Peter",
                    LastName = "Garcia",
                    PhoneNumber = "+380734679685",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user14",
                    Email = "user14@example.com",
                    FirstName = "Merry",
                    LastName = "Harrison",
                    PhoneNumber = "+380674079985",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                },
                new User
                {
                    UserName = "user15",
                    Email = "user15@example.com",
                    FirstName = "Tom",
                    LastName = "Smith",
                    PhoneNumber = "+380734673385",
                    PasswordHash = "MhD78WW9kjp5@JPpzvK9A"
                });
        }
    }
}
