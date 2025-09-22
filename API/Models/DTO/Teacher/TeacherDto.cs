using System;

namespace API.Models.DTO.Teacher;

public class TeacherDto
{
    public Guid Id { get; set; }
    public required string TeacherName { get; set; }
}
