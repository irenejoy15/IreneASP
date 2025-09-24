using System;
using API.Models.Domain;

namespace API.Models.DTO.Student;

public class CreateStudentDto
{
    public required string Name { get; set; }

    public ICollection<CreateStudentDetailDto> StudentDetails { get; set; } = new List<CreateStudentDetailDto>();
}
    