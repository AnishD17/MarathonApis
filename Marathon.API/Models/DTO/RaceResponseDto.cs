using Marathon.API.Models.Domain;

namespace Marathon.API.Models.DTO
{
	public class RaceResponseDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public int DistanceInKm { get; set; }
		public Region Region { get; set; }
		public Difficulty Difficulty { get; set; }
	}
}
