using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IRunnerRepository
	{
		Task <List<Runner>> GetAllRunnersAsync();
		Task<Runner?> GetRunnerByIdAsync (int id);
		Task<Runner> CreateRunnerRecord(Runner runner);
		Task<Runner?> UpdateRunnerRecordAsync (int id,Runner runner);
		Task<Runner?> DeleteRunnerRecordAsync (int id);
	}
}
