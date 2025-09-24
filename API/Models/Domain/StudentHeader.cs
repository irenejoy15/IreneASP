using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Domain;

public class StudentHeader
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    [ForeignKey("StudentHeaderId")]
    public ICollection<StudentDetail> StudentDetails { get; set; } = new List<StudentDetail>();
}
