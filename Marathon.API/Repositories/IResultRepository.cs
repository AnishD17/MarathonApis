using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IResultRepository
	{
		Task<List<Result>> GetAllResultRecordsAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
		Task<Result?> GetResultRecordsByIdAsync(int id);
		Task<Result> CreateResultRecordAsync(Result result);
		Task<Result?> UpdateResultRecordAsync(int id,Result result);
		Task<Result?> DeleteResultRecordAsync(int id);

	}
}
