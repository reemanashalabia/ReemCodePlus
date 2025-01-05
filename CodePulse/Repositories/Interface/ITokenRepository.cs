using Microsoft.AspNetCore.Identity;

namespace CodePulse.Repositories.Interface
{
    public interface ITokenRepository
    {

        string CreateJWTAsync(IdentityUser user, List<string> roles);
    }
}