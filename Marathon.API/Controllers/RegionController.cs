using AutoMapper;
using Marathon.API.CustomActionFilters;
using Marathon.API.Models.Domain;
using Marathon.API.Models.DTO;
using Marathon.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Marathon.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionController : ControllerBase
	{
		private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;

		public RegionController(IRegionRepository regionRepository,IMapper mapper) 
		{
			this.regionRepository=regionRepository;
			this.mapper=mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetRegionRecords([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
			[FromQuery] string? sortBy, [FromQuery] bool? isAscending,
			[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
		{
			var regionDomainRecords = await regionRepository.GetAllRegionsAsync(filterOn, filterQuery, sortBy,
					isAscending ?? true, pageNumber, pageSize);

			var regionResponse = mapper.Map<List<RegionResponseDto>>(regionDomainRecords);

			return Ok(regionResponse);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetRegionRecordById([FromRoute]int id)
		{
			var regionsDomainRecords = await regionRepository.GetRegionByIdAsync(id);

			if (regionsDomainRecords == null)
			{
				return NotFound();
			}

			var regionResponse = mapper.Map<RegionResponseDto>(regionsDomainRecords);
			return Ok(regionResponse);
		}
		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> AddRegionRecord([FromBody]RegionRequestDto regionRequestDto)
		{
			var regionDomainRecords = mapper.Map<Region>(regionRequestDto);

			regionDomainRecords = await regionRepository.CreateRegionRecordAsync(regionDomainRecords);

			var regionResponse = mapper.Map<RegionResponseDto>(regionDomainRecords);
			return CreatedAtAction(nameof(GetRegionRecordById), new {id = regionResponse.Id},regionResponse);
		}
		[HttpPut]
		[Route("{id:int}")]
		[ValidateModel]
		public async Task<IActionResult> UpdateRegionRecord([FromRoute]int id, [FromBody]RegionRequestDto regionRequestDto)
		{
			var regionDomainRecords = mapper.Map<Region>(regionRequestDto);

			regionDomainRecords = await regionRepository.UpdateRegionRecordAsync(id,regionDomainRecords);	

			if(regionDomainRecords == null)
			{
				return NotFound();
			}

			var regionResponse = mapper.Map<RegionResponseDto> (regionDomainRecords);
			return Ok(regionResponse);
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> RemoveRegionRecord([FromRoute]int id)
		{
			var regionDomainRecord = await regionRepository.DeleteRegionRecordAsync(id);

			if (regionDomainRecord == null) 
			{
				return NotFound();
			}

			var regionResponse = mapper.Map<RegionResponseDto>(regionDomainRecord);
			return Ok(regionResponse);
		}
	}
}
