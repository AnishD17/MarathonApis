namespace Marathon.API.Models.Domain
{
	public class Region
	{
		public int Id { get; set; }
		public string RegionName { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
