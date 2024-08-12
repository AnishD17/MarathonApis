using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IRegionRepository
	{
		Task<List<Region>> GetAllRegionsAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
		Task<Region?> GetRegionByIdAsync(int id);
		Task<Region> CreateRegionRecordAsync(Region region);
		Task<Region?> UpdateRegionRecordAsync(int id,Region region);
		Task<Region?> DeleteRegionRecordAsync(int id);
	}
}
