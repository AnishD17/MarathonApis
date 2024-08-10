using System.ComponentModel.DataAnnotations;

namespace Marathon.API.Models.DTO
{
	public class RegistrationRequestDto
	{
		public int RunnerId { get; set; }
		public int RaceId { get; set; }
		[Required]
		public DateTime RegistrationDate { get; set; }
		[Required]
		public string BibNumber { get; set; }
	}
}
