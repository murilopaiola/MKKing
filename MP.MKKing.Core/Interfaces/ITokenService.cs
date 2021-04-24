using MP.MKKing.Core.Models.Identity;

namespace MP.MKKing.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}