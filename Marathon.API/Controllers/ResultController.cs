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
	public class ResultController : ControllerBase
	{
		private readonly IResultRepository resultRepository;
		private readonly IMapper mapper;

		public ResultController(IResultRepository resultRepository, IMapper mapper)
		{
			this.resultRepository=resultRepository;
			this.mapper=mapper;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllResults([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
			[FromQuery] string? sortBy, [FromQuery] bool? isAscending,
			[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
		{
			var resultDomainRecords = await resultRepository.GetAllResultRecordsAsync(filterOn, filterQuery, sortBy,
					isAscending ?? true, pageNumber, pageSize);

			var resultResponse = mapper.Map<List<ResultResponseDto>>(resultDomainRecords);
			return Ok(resultResponse);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetResultById([FromRoute]int id)
		{
			var resultDomainRecords = await resultRepository.GetResultRecordsByIdAsync(id);

			if (resultDomainRecords == null) 
			{ 
				return NotFound();
			}

			var resultResponse = mapper.Map<ResultResponseDto>(resultDomainRecords);
			return Ok(resultResponse);
		}
		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> AddResultRecord([FromBody]ResultRequestDto resultRequestDto)
		{
			var resultDomainRecord = mapper.Map<Result>(resultRequestDto);

			resultDomainRecord = await resultRepository.CreateResultRecordAsync(resultDomainRecord);

			var resultResponse = mapper.Map<ResultResponseDto>(resultDomainRecord);

			return CreatedAtAction(nameof(GetResultById), new {id = resultResponse.Id},resultResponse);
		}
		[HttpPut]
		[Route("{id:int}")]
		[ValidateModel]
		public async Task<IActionResult> UpdateResultRecord([FromRoute]int id, [FromBody]ResultRequestDto resultRequestDto)
		{
			var resultDomainRecord = mapper.Map<Result>(resultRequestDto);

			resultDomainRecord = await resultRepository.UpdateResultRecordAsync(id,resultDomainRecord);

			if(resultDomainRecord == null)
			{
				return NotFound();
			}
			var resultResponse = mapper.Map<ResultResponseDto>(resultDomainRecord);
			return Ok(resultResponse);
		}
		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> DeleteResultRecord([FromRoute]int id)
		{
			var resultDomainRecord = await resultRepository.DeleteResultRecordAsync(id);

			if(resultDomainRecord == null)
			{
				return NotFound();
			}

			var resultResponse = mapper.Map<ResultResponseDto> (resultDomainRecord);
			return Ok(resultResponse);
		}
	}
}
