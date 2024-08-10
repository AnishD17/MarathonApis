using System.ComponentModel.DataAnnotations;

namespace Marathon.API.Models.DTO
{
	public class RunnerRecordRequestDto
	{
		[Required]
		[MaxLength(100, ErrorMessage = "First name has to be a maximum of 100 characters")]
		public string FirstName { get; set; }
		[Required]
		[MaxLength(100, ErrorMessage = "Last name has to be a maximum of 100 characters")]
		public string LastName { get; set; }
		[Required]
		public DateTime DateOfBirth { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
	}
}
