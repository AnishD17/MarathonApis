using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IRegionRepository
	{
		Task<List<Region>> GetAllRegionsAsync();
		Task<Region?> GetRegionByIdAsync(int id);
		Task<Region> CreateRegionRecordAsync(Region region);
		Task<Region?> UpdateRegionRecordAsync(int id,Region region);
		Task<Region?> DeleteRegionRecordAsync(int id);
	}
}
