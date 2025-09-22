using System;

namespace API.Models.DTO;
using System.ComponentModel.DataAnnotations;
public class UpdateSubjectDto
{
    [Required(ErrorMessage = "Subject Name is required")]
    [MinLength(3, ErrorMessage = "Subject Name must be at least 3 characters long")]
    [MaxLength(50, ErrorMessage = "Subject Name cannot exceed 50 characters")]
    public required string SubjectName { get; set; }

    [Required(ErrorMessage = "Subject Code is required")]
    public required string SubjectCode { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public required string Description { get; set; }
}
