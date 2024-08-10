using Microsoft.AspNetCore.Identity;

namespace Marathon.API.Repositories
{
	public interface ITokenRepository
	{
		string CreateJWTToken(IdentityUser user, List<string> roles);
	}
}
