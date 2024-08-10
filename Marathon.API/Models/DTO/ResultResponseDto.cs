using Marathon.API.Models.Domain;

namespace Marathon.API.Models.DTO
{
	public class ResultResponseDto
	{
		public int Id { get; set; }
		public TimeSpan? FinishTime { get; set; }
		public Registration Registration { get; set; }
	}
}
