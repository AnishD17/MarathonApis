using Marathon.API.Data;
using Marathon.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Marathon.API.Repositories
{
	public class SQLRaceRepository : IRaceRepository
	{
		private readonly MarathonDbContext dbContext;

		public SQLRaceRepository(MarathonDbContext dbContext)
        {
			this.dbContext=dbContext;
		}
        public async Task<Race> CreateRacesRecordAsync(Race race)
		{
			await dbContext.Races.AddAsync(race);
			await dbContext.SaveChangesAsync();
			return race;
		}

		public async Task<Race?> DeleteRacesRecordAsync(int id)
		{
			var existingRaceRecord = await dbContext.Races.FirstOrDefaultAsync(x => x.Id == id);
			if (existingRaceRecord == null)
			{
				return null;
			}
			dbContext.Races.Remove(existingRaceRecord);
			await dbContext.SaveChangesAsync();
			return existingRaceRecord;
		}

		public async Task<List<Race>> GetAllRacesAsync()
		{
			return await dbContext.Races.Include("Region").Include("Difficulty").ToListAsync();
		}

		public async Task<Race?> GetRacesByIdAsync(int id)
		{
			return await dbContext.Races.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x => x.Id == id);	

		}

		public async Task<Race?> UpdateRacesRecordAsync(int id, Race race)
		{
			var existingRaceRecord = await dbContext.Races.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRaceRecord == null)
			{
				return null;
			}
			race.Location = existingRaceRecord.Location;
			race.DifficultyId = existingRaceRecord.DifficultyId;
			race.Name = existingRaceRecord.Name;
			race.Date = existingRaceRecord.Date;
			race.RegionId = existingRaceRecord.RegionId;
			race.DifficultyId = existingRaceRecord.DifficultyId;

			await dbContext.SaveChangesAsync();
			return existingRaceRecord;
		}
	}
}
