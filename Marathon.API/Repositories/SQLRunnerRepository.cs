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

		public async Task<List<Runner>> GetAllRunnersAsync()
		{
			return await dbContext.Runners.ToListAsync();
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
