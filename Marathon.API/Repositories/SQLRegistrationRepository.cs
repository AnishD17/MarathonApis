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

		public async Task<List<Registration>> GetRegistrationsAsync()
		{
			return await DbContext.Registrations.Include("Runner").Include("Race").ToListAsync();
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
