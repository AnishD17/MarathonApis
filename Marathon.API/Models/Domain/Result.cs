namespace Marathon.API.Models.Domain
{
	public class Result
	{
		public int Id { get; set; }
		public int RegistrationId { get; set; }
		public TimeSpan? FinishTime { get; set; }

		public Registration Registration { get; set; }
	}
}
