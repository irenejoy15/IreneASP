using System;

namespace API.Models.Domain;

public class Subject
{
    public Guid Id { get; set; }
    public required string SubjectName { get; set; }
    public required string SubjectCode { get; set; }
    public required string Description { get; set; }
}
