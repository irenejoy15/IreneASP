using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Teacher;

public class CreateTeacherDto
{

    [Required]
    [MaxLength(50, ErrorMessage = "Teacher name cannot exceed 50 characters.")]
    [MinLength(2, ErrorMessage = "Teacher name must be at least 2 characters long.")]
    public required string TeacherName { get; set; }
}
