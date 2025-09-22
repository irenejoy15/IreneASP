using API.Data;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.CustomActionFilters;
using API.Models.DTO.Teacher;
using API.Models.Domain;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IreneDBContext dBContext;

        private readonly ITeacherRepository teacherRepository;
        private readonly IMapper mapper;
        private readonly ILogger<TeacherController> logger;

        public TeacherController(IreneDBContext dBContext, ITeacherRepository teacherRepository, IMapper mapper, ILogger<TeacherController> logger)
        {
            this.dBContext = dBContext;
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await teacherRepository.GetAllAsync();
            return Ok(teachers);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var teacher = await teacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddTeacher([FromBody] CreateTeacherDto createTeacherDto)
        {
            var teacherDomainModel = mapper.Map<Teacher>(createTeacherDto);
            teacherDomainModel = await teacherRepository.CreateAsync(teacherDomainModel);
            var teacherDto = mapper.Map<TeacherDto>(teacherDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = teacherDto.Id }, teacherDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateTeacher([FromRoute] Guid id, [FromBody] CreateTeacherDto createTeacherDto)
        {
            var teacherDomainModel = mapper.Map<Teacher>(createTeacherDto);
            teacherDomainModel = await teacherRepository.UpdateAsync(id, teacherDomainModel);
            if (teacherDomainModel == null)
            {
                return NotFound();
            }
            var teacherDto = mapper.Map<TeacherDto>(teacherDomainModel);
            return Ok(teacherDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] Guid id)
        {
            var deleted = await teacherRepository.DeleteAsync(id);
            if (deleted == null)
            {
                return NotFound();
            }

            var teacherDto = mapper.Map<TeacherDto>(deleted);
            return Ok(teacherDto);
        }

    }

}
