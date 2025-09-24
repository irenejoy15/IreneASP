using System;

namespace API.Models.Domain;

public class StudentHeader
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<StudentDetail> StudentDetails { get; set; } = new List<StudentDetail>();
}
