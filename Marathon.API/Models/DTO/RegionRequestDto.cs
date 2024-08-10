using System.ComponentModel.DataAnnotations;

namespace Marathon.API.Models.DTO
{
	public class RegionRequestDto
	{
		[Required]
		[MaxLength(100, ErrorMessage = "Region name has to be a maximum of 100 characters")]
		public string RegionName { get; set; }
		[Required]
		[MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
		[MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")]
		public string Code { get; set; }
		[MaxLength(200, ErrorMessage = "Name has to be a maximum of 100 characters")]
		public string? Description { get; set; }
		public string? RegionImageUrl { get; set; }
	}
}
