using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MP.MKKing.Core.Models.Identity;

namespace MP.MKKing.API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserFromClaimsPrincipal(this UserManager<AppUser> input, ClaimsPrincipal user, bool includeAddress = false)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            if (!includeAddress)
                return await input.Users.SingleOrDefaultAsync(x => x.Email == email);

            return await input.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}