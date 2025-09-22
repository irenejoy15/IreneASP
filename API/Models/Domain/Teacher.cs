using System;

namespace API.Models.Domain;

public class Teacher
{
    public Guid Id { get; set; }
    public required string TeacherName { get; set; }
}
