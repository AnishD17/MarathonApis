using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IRegistrationRepository
	{
		Task<List<Registration>> GetRegistrationsAsync(); 
		Task<Registration?> GetRegistrationByIdAsync(int id);
		Task<Registration> CreateRegistrationRecordAsync(Registration registration);
		Task<Registration?> UpdateRegistrationAsync(int id,Registration registration);
		Task<Registration?> DeleteRegistrationAsync(int id);
	}
}
