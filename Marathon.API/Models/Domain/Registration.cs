using System.Diagnostics;

namespace Marathon.API.Models.Domain
{
	public class Registration
	{
		public int Id { get; set; }
		public int RunnerId { get; set; }
		public int RaceId { get; set; }
		public DateTime RegistrationDate { get; set; }
		public string BibNumber { get; set; }

		public Runner Runner { get; set; }
		public Race Race { get; set; }
	}
}
