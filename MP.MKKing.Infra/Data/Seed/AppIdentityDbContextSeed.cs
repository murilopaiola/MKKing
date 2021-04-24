using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MP.MKKing.Core.Models.Identity;

namespace MP.MKKing.Infra.Data.Seed
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address()
                    {
                        FirstName = "Bob",
                        LastName = "Bobbity",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}