using Marathon.API.Data;
using Marathon.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Marathon.API.Repositories
{
	public class SQLRunnerRepository : IRunnerRepository
	{
		private readonly MarathonDbContext dbContext;

		public SQLRunnerRepository(MarathonDbContext dbContext)
        {
			this.dbContext=dbContext;
		}

		public async Task<Runner> CreateRunnerRecord(Runner runner)
		{
			await dbContext.Runners.AddAsync(runner);
			await dbContext.SaveChangesAsync();
			return runner;
		}

		public async Task<Runner?> DeleteRunnerRecordAsync(int id)
		{
			var existingRunnerRecord = await dbContext.Runners.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRunnerRecord == null)
			{ 
				return null;
			}
			dbContext.Runners.Remove(existingRunnerRecord);
			await dbContext.SaveChangesAsync();
			return existingRunnerRecord;
		}

		public async Task<List<Runner>> GetAllRunnersAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
		{
			var runners = dbContext.Runners.AsQueryable();

			// Filtering
			if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
			{
				if (filterOn.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
				{
					runners = runners.Where(x => x.FirstName.Contains(filterQuery));
				}
				else if (filterOn.Equals("LastName", StringComparison.OrdinalIgnoreCase))
				{
					runners = runners.Where(x => x.LastName.Contains(filterQuery));
				}
			}

			// Sorting 
			if (string.IsNullOrWhiteSpace(sortBy) == false)
			{
				if (sortBy.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
				{
					runners = isAscending ? runners.OrderBy(x => x.FirstName) : runners.OrderByDescending(x => x.FirstName);
				}
				else if (sortBy.Equals("LastName", StringComparison.OrdinalIgnoreCase))
				{
					runners = isAscending ? runners.OrderBy(x => x.LastName) : runners.OrderByDescending(x => x.LastName);
				}
			}

			// Pagination
			var skipResults = (pageNumber - 1) * pageSize;

			return await runners.Skip(skipResults).Take(pageSize).ToListAsync();
			//return await dbContext.Runners.ToListAsync();
		}

		public async Task<Runner?> GetRunnerByIdAsync(int id)
		{
			return await dbContext.Runners.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Runner?> UpdateRunnerRecordAsync(int id, Runner runner)
		{
			var existingRunnerRecord = await dbContext.Runners.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRunnerRecord == null)
			{ 
				return null;
			}	

			existingRunnerRecord.FirstName = runner.FirstName;
			existingRunnerRecord.LastName = runner.LastName;
			existingRunnerRecord.DateOfBirth = runner.DateOfBirth;
			existingRunnerRecord.Email = runner.Email;
			existingRunnerRecord.PhoneNumber = runner.PhoneNumber;

			await dbContext.SaveChangesAsync();
			return existingRunnerRecord;
		}
	}
}
