using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IResultRepository
	{
		Task<List<Result>> GetAllResultRecordsAsync();
		Task<Result?> GetResultRecordsByIdAsync(int id);
		Task<Result> CreateResultRecordAsync(Result result);
		Task<Result?> UpdateResultRecordAsync(int id,Result result);
		Task<Result?> DeleteResultRecordAsync(int id);

	}
}
