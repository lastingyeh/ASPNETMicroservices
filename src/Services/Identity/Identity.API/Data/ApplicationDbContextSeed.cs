using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.API.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, ILogger<ApplicationDbContextSeed> logger)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(GetPreconfiguredUsers());

                await context.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }
        }

        private static IEnumerable<ApplicationUser> GetPreconfiguredUsers()
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser
            {
                CardHolderName = "DemoUser",
                CardNumber = "4012888888881881",
                CardType = 1,
                City = "Redmond",
                Country = "U.S.",
                Email = "demouser@microsoft.com",
                Expiration = "12/21",
                Id = Guid.NewGuid().ToString(),
                LastName = "DemoLastName",
                Name = "DemoUser",
                PhoneNumber = "1234567890",
                UserName = "demouser@microsoft.com",
                ZipCode = "98052",
                State = "WA",
                Street = "15703 NE 61st Ct",
                SecurityNumber = "535",
                NormalizedEmail = "DEMOUSER@MICROSOFT.COM",
                NormalizedUserName = "DEMOUSER@MICROSOFT.COM",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            user.PasswordHash = passwordHasher.HashPassword(user, "Pass@word1");

            return new List<ApplicationUser> { user };
        }
    }
}