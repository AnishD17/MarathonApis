using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IRaceRepository
	{
		Task<List<Race>> GetAllRacesAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
		Task<Race?> GetRacesByIdAsync(int id);
		Task<Race> CreateRacesRecordAsync(Race race);
		Task<Race?> UpdateRacesRecordAsync(int id, Race race);
		Task<Race?> DeleteRacesRecordAsync(int id);
	}
}
