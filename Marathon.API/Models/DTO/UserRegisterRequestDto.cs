using System.ComponentModel.DataAnnotations;

namespace Marathon.API.Models.DTO
{
	public class UserRegisterRequestDto
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public string[] Roles { get; set; }
	}
}
