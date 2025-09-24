using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.DTO.Student;

public class StudentHeaderDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    [ForeignKey("StudentHeaderId")]
    public ICollection<StudentDetailDto> StudentDetails { get; set; } = new List<StudentDetailDto>();
}
