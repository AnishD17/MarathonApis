using AutoMapper;
using Marathon.API.CustomActionFilters;
using Marathon.API.Models.Domain;
using Marathon.API.Models.DTO;
using Marathon.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
		public async Task<IActionResult> GetRunnerRecord()
		{
			var runnerDomainRecords = await runnerRepository.GetAllRunnersAsync();

			throw new Exception("Something went wrong");

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
