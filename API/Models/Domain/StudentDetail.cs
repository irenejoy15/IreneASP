using System;
using API.Models.DTO;
using API.Models.DTO.Teacher;

namespace API.Models.Domain;

public class StudentDetail
{
    public Guid Id { get; set; }
    public required int Grade { get; set; }

    public Guid TeacherId { get; set; }
    public Guid SubjectId { get; set; }
    public Guid StudentHeaderId { get; set; }
    public required Teacher? Teacher { get; set; }
    public required Subject? Subject { get; set; }
    public required StudentHeader? StudentHeader { get; set; }
}
