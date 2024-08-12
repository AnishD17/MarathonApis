using Marathon.API.Data;
using Marathon.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Marathon.API.Repositories
{
	public class SQLRegistrationRepository : IRegistrationRepository
	{
		private readonly MarathonDbContext DbContext;

		public SQLRegistrationRepository(MarathonDbContext DbContext) 
		{
			this.DbContext=DbContext;
		}
		public async Task<Registration?> DeleteRegistrationAsync(int id)
		{
			var existingRecord = await DbContext.Registrations.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRecord == null)
			{
				return null;
			}
			
			DbContext.Registrations.Remove(existingRecord);
			await DbContext.SaveChangesAsync();
			return existingRecord;
		}

		public async Task<Registration?> GetRegistrationByIdAsync(int id)
		{
			return await DbContext.Registrations.Include("Runner").Include("Race").FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Registration> CreateRegistrationRecordAsync(Registration registration)
		{
			await DbContext.Registrations.AddAsync(registration);
			await DbContext.SaveChangesAsync();
			return registration;

		}

		public async Task<List<Registration>> GetRegistrationsAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
		{
			var registrations = DbContext.Registrations.Include("Runner").Include("Race").AsQueryable();

			// Filtering
			if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
			{
				if (filterOn.Equals("RegistrationDate", StringComparison.OrdinalIgnoreCase))
				{
					registrations = registrations.Where(x => x.RegistrationDate.ToString().Contains(filterQuery));
				}
				else if (filterOn.Equals("BibNumber", StringComparison.OrdinalIgnoreCase))
				{
					registrations = registrations.Where(x => x.BibNumber.Contains(filterQuery));
				}
			}

			// Sorting 
			if (string.IsNullOrWhiteSpace(sortBy) == false)
			{
				if (sortBy.Equals("RegistrationDate", StringComparison.OrdinalIgnoreCase))
				{
					registrations = isAscending ? registrations.OrderBy(x => x.RegistrationDate) : registrations.OrderByDescending(x => x.RegistrationDate);
				}
				if (sortBy.Equals("BibNumber", StringComparison.OrdinalIgnoreCase))
				{
					registrations = isAscending ? registrations.OrderBy(x => x.BibNumber) : registrations.OrderByDescending(x => x.BibNumber);
				}
			}

			// Pagination
			var skipResults = (pageNumber - 1) * pageSize;

			return await registrations.Skip(skipResults).Take(pageSize).ToListAsync();
			//return await DbContext.Registrations.Include("Runner").Include("Race").ToListAsync();
		}

		public async Task<Registration?> UpdateRegistrationAsync(int id, Registration registration)
		{
			var existingRecords = await DbContext.Registrations.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRecords == null)
			{ 
				return null;
			}

			existingRecords.RunnerId = registration.RunnerId;
			existingRecords.RaceId = registration.RaceId;
			existingRecords.RegistrationDate = registration.RegistrationDate;
			existingRecords.BibNumber = registration.BibNumber;

			await DbContext.SaveChangesAsync();
			return existingRecords;
		}
	}
}
