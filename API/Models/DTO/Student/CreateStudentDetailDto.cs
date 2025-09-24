using System;
using API.Models.Domain;
using API.Models.DTO.Teacher;

namespace API.Models.DTO.Student;

public class CreateStudentDetailDto
{
    public required int Grade { get; set; }
    public Guid TeacherId { get; set; }
    public Guid SubjectId { get; set; }
    public Guid StudentHeaderId { get; set; }
}
