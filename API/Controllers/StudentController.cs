using API.Data;
using API.Models.Domain;
using API.Models.DTO.Student;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IreneDBContext dBContext;
        private readonly IStudentRepository studentRepository;
        private readonly IStudentDetailRepository studentDetailRepository;
        private readonly ILogger<StudentController> logger;
        private readonly IMapper mapper;

        public StudentController(IreneDBContext dBContext, IStudentRepository studentRepository, IStudentDetailRepository studentDetailRepository, ILogger<StudentController> logger, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.studentRepository = studentRepository;
            this.studentDetailRepository = studentDetailRepository;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var studentHeader = await studentRepository.GetByIdAsync(id);
            if (studentHeader == null) return NotFound();

            var studentHeaderDto = mapper.Map<StudentHeaderDto>(studentHeader);
            return Ok(studentHeaderDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentHeader([FromBody] CreateStudentDto createStudentHeaderDto)
        {
            var studentHeaderDomainModel = mapper.Map<StudentHeader>(createStudentHeaderDto);
            studentHeaderDomainModel = await studentRepository.CreateAsync(studentHeaderDomainModel);
            var studentHeaderDto = mapper.Map<StudentHeaderDto>(studentHeaderDomainModel);
            return Ok(studentHeaderDto);

        }
    }
}
