using AutoMapper;
using Marathon.API.CustomActionFilters;
using Marathon.API.Models.Domain;
using Marathon.API.Models.DTO;
using Marathon.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marathon.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RaceController : ControllerBase
	{
		private readonly IRaceRepository raceRepository;
		private readonly IMapper mapper;

		public RaceController(IRaceRepository raceRepository,IMapper mapper)
        {
			this.raceRepository=raceRepository;
			this.mapper=mapper;
		}
        [HttpGet]
		public async Task<IActionResult> GetRaceRecords([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
			[FromQuery] string? sortBy, [FromQuery] bool? isAscending,
			[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
		{
			var raceRecords = await raceRepository.GetAllRacesAsync(filterOn, filterQuery, sortBy,
					isAscending ?? true, pageNumber, pageSize);

			var raceResponse = mapper.Map<List<RaceResponseDto>>(raceRecords);

			return Ok(raceResponse);
		}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetRaceRecordById([FromRoute]int id) 
		{ 
			var raceRecords = await raceRepository.GetRacesByIdAsync(id);	

			var raceResponse = mapper.Map<RaceResponseDto>(raceRecords);
			return Ok(raceResponse);
		
		}
		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> AddRaceRecord([FromBody]RaceRequestDto raceRequestDto)
		{
			var raceDomainRecord = mapper.Map<Race>(raceRequestDto);

			raceDomainRecord = await raceRepository.CreateRacesRecordAsync(raceDomainRecord);

			var raceResponse = mapper.Map<RaceResponseDto>(raceDomainRecord);

			return CreatedAtAction(nameof(GetRaceRecordById), new {id = raceResponse.Id} ,raceResponse);
		}
		[HttpPut]
		[Route("{id:int}")]
		[ValidateModel]
		public async Task<IActionResult> UpdateRaceRecord([FromRoute]int id, [FromBody]RaceRequestDto raceRequestDto)
		{
			var raceDomainRecords = mapper.Map<Race>(raceRequestDto);

			raceDomainRecords = await raceRepository.UpdateRacesRecordAsync(id, raceDomainRecords);

			if (raceDomainRecords == null)
			{
				return NotFound();
			}

			var raceResponse = mapper.Map<RaceResponseDto>(raceDomainRecords);
			return Ok(raceResponse);
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> RemoveRaceRecord([FromRoute]int id)
		{
			var raceDomainRecords = await raceRepository.DeleteRacesRecordAsync(id);

			if(raceDomainRecords == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<RaceResponseDto>(raceDomainRecords));

		}
	}
}
