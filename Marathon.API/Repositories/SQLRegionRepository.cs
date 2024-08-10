using Marathon.API.Data;
using Marathon.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Marathon.API.Repositories
{
	public class SQLRegionRepository : IRegionRepository
	{
		private readonly MarathonDbContext DbContext;

		public SQLRegionRepository(MarathonDbContext DbContext)
        {
			this.DbContext=DbContext;
		}
        public async Task<Region> CreateRegionRecordAsync(Region region)
		{
			await DbContext.Regions.AddAsync(region);
			await DbContext.SaveChangesAsync();
			return region;
		}

		public async Task<Region?> DeleteRegionRecordAsync(int id)
		{
			var existingRegionRecord = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRegionRecord == null)
			{
				return null;
			}

			DbContext.Regions.Remove(existingRegionRecord);
			await DbContext.SaveChangesAsync();
			return existingRegionRecord;

		}

		public async Task<List<Region>> GetAllRegionsAsync()
		{
			return await DbContext.Regions.ToListAsync();	
		}

		public async Task<Region?> GetRegionByIdAsync(int id)
		{
			return await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Region?> UpdateRegionRecordAsync(int id, Region region)
		{
			var existingRegionRecord = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRegionRecord == null)
			{
				return null;
			}

			existingRegionRecord.RegionName = region.RegionName;
			existingRegionRecord.Code = region.Code;
			existingRegionRecord.Description = region.Description;
			existingRegionRecord.RegionImageUrl = region.RegionImageUrl;

			await DbContext.SaveChangesAsync();
			return existingRegionRecord;
		}
	}
}
