using System;
using API.Models.Domain;
using API.Models.DTO.Teacher;

namespace API.Models.DTO.Student;

public class StudentDetailDto
{
    public Guid Id { get; set; }
    public required int Grade { get; set; }

    public required TeacherDto? Teacher { get; set; }
    public required SubjectDto? Subject { get; set; }
    public required StudentHeaderDto? StudentHeader { get; set; }
}
