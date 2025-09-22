using System;

namespace API.Models.DTO;

public class SubjectDto
{
    public Guid Id { get; set; }
    public required string SubjectName { get; set; }
    public required string SubjectCode { get; set; }
    public required string Description { get; set; }
}
