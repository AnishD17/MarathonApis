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

		public async Task<List<Region>> GetAllRegionsAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
		{
			var regions = DbContext.Regions.AsQueryable();

			// Filtering
			if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
			{
				if (filterOn.Equals("RegionName", StringComparison.OrdinalIgnoreCase))
				{
					regions = regions.Where(x => x.RegionName.Contains(filterQuery));
				}
				else if (filterOn.Equals("Code", StringComparison.OrdinalIgnoreCase))
				{
					regions = regions.Where(x => x.Code.Contains(filterQuery));
				}
			}

			// Sorting 
			if (string.IsNullOrWhiteSpace(sortBy) == false)
			{
				if (sortBy.Equals("RegionName", StringComparison.OrdinalIgnoreCase))
				{
					regions = isAscending ? regions.OrderBy(x => x.RegionName) : regions.OrderByDescending(x => x.RegionName);
				}
				else if (sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
				{
					regions = isAscending ? regions.OrderBy(x => x.Code) : regions.OrderByDescending(x => x.Code);
				}
			}

			// Pagination
			var skipResults = (pageNumber - 1) * pageSize;

			return await regions.Skip(skipResults).Take(pageSize).ToListAsync();
			//return await DbContext.Regions.ToListAsync();	
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
