using AutoMapper;
using Marathon.API.CustomActionFilters;
using Marathon.API.Models.Domain;
using Marathon.API.Models.DTO;
using Marathon.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Marathon.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RunnerController : ControllerBase
	{
		private readonly IRunnerRepository runnerRepository;
		private readonly IMapper mapper;
		private readonly ILogger<RunnerController> logger;

		public RunnerController(IRunnerRepository runnerRepository,IMapper mapper,ILogger<RunnerController> logger)
        {
			this.runnerRepository=runnerRepository;
			this.mapper=mapper;
			this.logger=logger;
		}

		//GET: /api/Runner? filterOn = Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
		[HttpGet]
		public async Task<IActionResult> GetRunnerRecord([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
			[FromQuery] string? sortBy, [FromQuery] bool? isAscending,
			[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
		{
			var runnerDomainRecords = await runnerRepository.GetAllRunnersAsync(filterOn, filterQuery, sortBy,
					isAscending ?? true, pageNumber, pageSize);

			var runnerResponse = mapper.Map<List<RunnerResponseDto>>(runnerDomainRecords);
			return Ok(runnerDomainRecords);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetRunnerRecordById([FromRoute]int id)
		{
			var runnerDomainRecord = await runnerRepository.GetRunnerByIdAsync(id);

			if (runnerDomainRecord == null)
			{
				return NotFound();
			}

			var runnerResponse = mapper.Map<RunnerResponseDto>(runnerDomainRecord);
			return Ok(runnerResponse);
		}

		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> CreateRunnerRecord([FromBody]RunnerRecordRequestDto runnerRecordRequestDto)
		{
			var runnerDomainRecord = mapper.Map<Runner>(runnerRecordRequestDto);

		    runnerDomainRecord = await runnerRepository.CreateRunnerRecord(runnerDomainRecord);

			var runnerResponse = mapper.Map<RunnerResponseDto>(runnerDomainRecord);

			return CreatedAtAction(nameof(GetRunnerRecordById), new {id = runnerResponse.Id},runnerResponse);
		}

		[HttpPut]
		[Route("{id:int}")]
		[ValidateModel]
		public async Task<IActionResult> UpdateRunnerRecord([FromRoute] int id,[FromBody] RunnerRecordRequestDto runnerRecordRequestDto)
		{
			var runnerDomainRecord = mapper.Map<Runner>(runnerRecordRequestDto);

			runnerDomainRecord = await runnerRepository.UpdateRunnerRecordAsync(id, runnerDomainRecord);

			if (runnerDomainRecord == null)
			{
				return NotFound();
			}

			var runnerResponse = mapper.Map<RunnerResponseDto>(runnerDomainRecord); 

			return Ok(runnerResponse);
		}
		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> DeleteRunnerRecord([FromRoute]int id)
		{
			var runnerDomainRecord = await runnerRepository.DeleteRunnerRecordAsync(id);	

			if(runnerDomainRecord == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<RunnerResponseDto>(runnerDomainRecord)); 
		}

	}
}
