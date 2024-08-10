namespace Marathon.API.Models.Domain
{
	public class Race
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public int RegionId { get; set; }
		public int DifficultyId { get; set; }

		public Region Region { get; set; }
		public Difficulty Difficulty { get; set; }
	}
}
