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

		public async Task<List<Race>> GetAllRacesAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
		{
			var races = dbContext.Races.Include("Region").Include("Difficulty").AsQueryable();

			// Filtering
			if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
			{
				if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					races = races.Where(x =>x.Name.Contains(filterQuery));
				}
				else if (filterOn.Equals("Location", StringComparison.OrdinalIgnoreCase))
				{
					races = races.Where(x => x.Location.Contains(filterQuery));
				}
				else if (filterOn.Equals("Distance", StringComparison.OrdinalIgnoreCase))
				{
					races = races.Where(x => x.DistanceInKm.ToString().Contains(filterQuery));
				}
				else if (filterOn.Equals("Date", StringComparison.OrdinalIgnoreCase))
				{
					races = races.Where(x => x.Date.ToString().Contains(filterQuery));
				}
			}

			// Sorting 
			if (string.IsNullOrWhiteSpace(sortBy) == false)
			{
				if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					races = isAscending ? races.OrderBy(x => x.Name) : races.OrderByDescending(x => x.Name);
				}
				else if (sortBy.Equals("Location", StringComparison.OrdinalIgnoreCase))
				{
					races = isAscending ? races.OrderBy(x => x.Location) : races.OrderByDescending(x => x.Location);
				}
				else if (sortBy.Equals("Distance", StringComparison.OrdinalIgnoreCase))
				{
					races = isAscending ? races.OrderBy(x => x.DistanceInKm) : races.OrderByDescending(x => x.DistanceInKm);
				}
				else if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
				{
					races = isAscending ? races.OrderBy(x => x.Date) : races.OrderByDescending(x => x.Date);
				}
			}

			// Pagination
			var skipResults = (pageNumber - 1) * pageSize;

			return await races.Skip(skipResults).Take(pageSize).ToListAsync();
			//return await dbContext.Races.Include("Region").Include("Difficulty").ToListAsync();
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
