using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IRaceRepository
	{
		Task<List<Race>> GetAllRacesAsync();
		Task<Race?> GetRacesByIdAsync(int id);
		Task<Race> CreateRacesRecordAsync(Race race);
		Task<Race?> UpdateRacesRecordAsync(int id, Race race);
		Task<Race?> DeleteRacesRecordAsync(int id);
	}
}
