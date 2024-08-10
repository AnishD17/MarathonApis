using Marathon.API.Models.Domain;

namespace Marathon.API.Repositories
{
	public interface IImageRepository
	{
		Task<Image> Upload(Image image);
	}
}
