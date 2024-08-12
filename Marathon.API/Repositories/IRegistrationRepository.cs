using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IRegistrationRepository
	{
		Task<List<Registration>> GetRegistrationsAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000); 
		Task<Registration?> GetRegistrationByIdAsync(int id);
		Task<Registration> CreateRegistrationRecordAsync(Registration registration);
		Task<Registration?> UpdateRegistrationAsync(int id,Registration registration);
		Task<Registration?> DeleteRegistrationAsync(int id);
	}
}
