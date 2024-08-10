using AutoMapper;
using Marathon.API.Models.Domain;
using Marathon.API.Models.DTO;

namespace Marathon.API.Mappings
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles() 
		{
			CreateMap<Runner, RunnerRecordRequestDto>().ReverseMap();
			CreateMap<Runner, RunnerResponseDto>().ReverseMap();
			CreateMap<Region,RegionResponseDto>().ReverseMap();
			CreateMap<Region, RegionRequestDto>().ReverseMap();
			CreateMap<Race,RaceResponseDto>().ReverseMap();
			CreateMap<Race, RaceRequestDto>().ReverseMap();
			CreateMap<Registration, RegistrationResponseDto>().ReverseMap();
			CreateMap<Registration,RegionRequestDto>().ReverseMap();
			CreateMap<Result, ResultResponseDto>().ReverseMap();
			CreateMap<Result,ResultRequestDto>().ReverseMap();

		}
	}
}
