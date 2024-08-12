using Marathon.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Marathon.API.Models.DTO
{
	public class RaceRequestDto
	{
		[Required]
		[MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
		public string Name { get; set; }
		[Required]
		public DateTime Date { get; set; }
		[Required]
		[MaxLength(100, ErrorMessage = "Location has to be a maximum of 100 characters")]
		public string Location { get; set; }
		[Required]
		public int DistanceInKm { get; set; }
		public int RegionId { get; set; }
		public int DifficultyId { get; set; }
	}
}
