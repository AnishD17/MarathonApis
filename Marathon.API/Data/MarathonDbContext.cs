using Marathon.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Marathon.API.Data
{
	public class MarathonDbContext : DbContext
	{
        public MarathonDbContext(DbContextOptions<MarathonDbContext> dbContextOptions) : base(dbContextOptions)
        {
                
        }

        public DbSet<Difficulty> Difficulties { get; set; }
		public DbSet<Race> Races { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Registration> Registrations { get; set; }
		public DbSet<Result> Results { get; set; }
		public DbSet<Runner> Runners { get; set; }
		public DbSet<Image> Images { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var difficultiesEntries = new List<Difficulty>()
			{
				new Difficulty()
				{
					Id = 1,
					DifficultyLevel="Easy"
				},
				new Difficulty()
				{
					Id = 2,
					DifficultyLevel="Medium"
				},
				new Difficulty()
				{
				    Id = 3,
					DifficultyLevel="Hard"
				}
			};
			modelBuilder.Entity<Difficulty>().HasData(difficultiesEntries);
		}
	}
}
