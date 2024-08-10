using Marathon.API.Data;
using Marathon.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Marathon.API.Repositories
{
	public class SQLResultRepository : IResultRepository
	{
		private readonly MarathonDbContext dbContext;

		public SQLResultRepository(MarathonDbContext dbContext) 
		{
			this.dbContext=dbContext;
		}
		public async Task<Result> CreateResultRecordAsync(Result result)
		{
			await dbContext.Results.AddAsync(result);
			await dbContext.SaveChangesAsync();
			return result;
		}

		public async Task<Result?> DeleteResultRecordAsync(int id)
		{
			var existingRecord = await dbContext.Results.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRecord == null)
			{
				return null;
			}

			dbContext.Results.Remove(existingRecord);
			await dbContext.SaveChangesAsync();
			return existingRecord;
		}

		public async Task<List<Result>> GetAllResultRecordsAsync()
		{
			return await dbContext.Results.Include("Registration.Runner").Include("Registration.Race").ToListAsync();
		}

		public async Task<Result?> GetResultRecordsByIdAsync(int id)
		{
			return await dbContext.Results.Include("Registration.Runner").Include("Registration.Race").FirstOrDefaultAsync(x => x.Id==id);
		}

		public async Task<Result?> UpdateResultRecordAsync(int id, Result result)
		{
			var existingRecord = await dbContext.Results.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRecord == null)
			{ 
				return null;
			}

			existingRecord.FinishTime = result.FinishTime;
			existingRecord.RegistrationId = result.RegistrationId;

			await dbContext.SaveChangesAsync();
			return existingRecord;
		}
	}
}
