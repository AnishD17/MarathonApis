using Marathon.API.Models.Domain;

namespace Marathon.API.Models.DTO
{
	public class RegistrationResponseDto
	{
		public int Id { get; set; }
		public DateTime RegistrationDate { get; set; }
		public string BibNumber { get; set; }
		public Runner Runner { get; set; }
		public Race Race { get; set; }
	}
}
