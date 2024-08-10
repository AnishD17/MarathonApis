using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Marathon.API.Data
{
	public class MarathonAuthDbContext : IdentityDbContext
	{
		public MarathonAuthDbContext(DbContextOptions<MarathonAuthDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var userRoleId = "55f9cdd6-0a91-4659-a8a5-95eaba8b015b";
			var adminRoleId = "75b1943e-b729-4bd6-aa75-939c87cfdfb2";

			var roles = new List<IdentityRole>
			{

				new IdentityRole
				{
					Id = userRoleId,
					ConcurrencyStamp = userRoleId,
					Name = "User",
					NormalizedName = "User".ToUpper()
				},
				new IdentityRole
				{
					Id = adminRoleId,
					ConcurrencyStamp = adminRoleId,
					Name = "Admin",
					NormalizedName = "Admin".ToUpper()
				}

			};
			builder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
