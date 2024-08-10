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
	public class RegistrationController : ControllerBase
	{
		private readonly IRegistrationRepository registrationRepository;
		private readonly IMapper mapper;

		public RegistrationController(IRegistrationRepository registrationRepository,IMapper mapper)
		{
			this.registrationRepository=registrationRepository;
			this.mapper=mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetRegistrationRecords()
		{
			var registrationDomainRecords = await registrationRepository.GetRegistrationsAsync();

			var registrationResponse = mapper.Map<List<RegistrationResponseDto>>(registrationDomainRecords);
			return Ok(registrationResponse);
		}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetRegistrationRecordById([FromRoute]int id)
		{
			var registrationDomainRecords = await registrationRepository.GetRegistrationByIdAsync(id);

			if (registrationDomainRecords == null)
			{
				return NotFound();
			}

			var registrationResponse = mapper.Map<RegistrationResponseDto>(registrationDomainRecords);
			return Ok(registrationResponse);
		}

		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> AddRegistrationRecord([FromBody]RegistrationRequestDto registrationRequest)
		{
			var registrationDomainRecord = mapper.Map<Registration>(registrationRequest);

			registrationDomainRecord = await registrationRepository.CreateRegistrationRecordAsync(registrationDomainRecord);

			var registrationResponse = mapper.Map<RegistrationResponseDto>(registrationDomainRecord);

			return CreatedAtAction(nameof(GetRegistrationRecordById),new {id = registrationResponse.Id} ,registrationResponse);
		}
		[HttpPut]
		[Route("{id:int}")]
		[ValidateModel]
		public async Task<IActionResult> UpdateRegistrationRecord([FromRoute]int id, [FromBody]RegistrationRequestDto registrationRequestDto)
		{
			var registrationDomainRecords = mapper.Map<Registration>(registrationRequestDto);

			registrationDomainRecords = await registrationRepository.UpdateRegistrationAsync(id, registrationDomainRecords);

			if (registrationDomainRecords == null)
			{
				return NotFound();
			}
			var registrationResponse = mapper.Map<RegistrationResponseDto> (registrationDomainRecords);
			return Ok(registrationResponse);
		}
		[HttpDelete]
		[Route("id:int")]
		public async Task<IActionResult> DeleteRegistrationRecord([FromRoute]int id)
		{
			var registrationDomainRecord = await registrationRepository.DeleteRegistrationAsync(id);

			if(registrationDomainRecord == null)
			{
				return NotFound();
			}
			var registrationResponse = mapper.Map<RegistrationResponseDto>(registrationDomainRecord);
			return Ok(registrationResponse);
		}
	}
}
