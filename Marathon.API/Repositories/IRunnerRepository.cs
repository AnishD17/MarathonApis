using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IRunnerRepository
	{
		Task <List<Runner>> GetAllRunnersAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
		Task<Runner?> GetRunnerByIdAsync (int id);
		Task<Runner> CreateRunnerRecord(Runner runner);
		Task<Runner?> UpdateRunnerRecordAsync (int id,Runner runner);
		Task<Runner?> DeleteRunnerRecordAsync (int id);
	}
}
