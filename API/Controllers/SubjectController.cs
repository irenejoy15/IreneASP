using API.CustomActionFilters;
using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IreneDBContext dBContext;
        private readonly ISubjectRepository subjectRepository;

        private readonly IMapper mapper;
        private readonly ILogger<SubjectController> logger;

        public SubjectController(IreneDBContext dBContext, ISubjectRepository subjectRepository, IMapper mapper, ILogger<SubjectController> logger)
        {
            this.dBContext = dBContext;
            this.subjectRepository = subjectRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAll()
        {
            var subjects = await subjectRepository.GetAllAsync();
            var subjectDto = mapper.Map<List<SubjectDto>>(subjects);
            return Ok(subjectDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var subject = await subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            var subjectDto = mapper.Map<SubjectDto>(subject);
            return Ok(subjectDto);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        public async Task<IActionResult> AddSubject([FromBody] CreateSubjectDto createSubjectDto)
        {
            var subjectDomainModel = mapper.Map<Subject>(createSubjectDto);
            subjectDomainModel = await subjectRepository.CreateAsync(subjectDomainModel);
            var SubjectDto = mapper.Map<SubjectDto>(subjectDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = SubjectDto.Id }, SubjectDto);

        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateSubject([FromRoute] Guid id, [FromBody] UpdateSubjectDto updateSubjectDto)
        {
            var subjectDomainModel = mapper.Map<Subject>(updateSubjectDto);
            subjectDomainModel = await subjectRepository.UpdateAsync(id, subjectDomainModel);
            if (subjectDomainModel == null)
            {
                return NotFound();
            }
            var SubjectDto = mapper.Map<SubjectDto>(subjectDomainModel);
            return Ok(SubjectDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteSubject([FromRoute] Guid id)
        {
            var subjectDomainModel = await subjectRepository.DeleteAsync(id);
            if (subjectDomainModel == null)
            {
                return NotFound();
            }
            var SubjectDto = mapper.Map<SubjectDto>(subjectDomainModel);
            return Ok(SubjectDto);
        }
    }
}
