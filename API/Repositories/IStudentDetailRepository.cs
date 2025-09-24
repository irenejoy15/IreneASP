using System;
using API.Models.Domain;

namespace API.Repositories;

public interface IStudentDetailRepository
{
    Task<StudentDetail?> GetByIdAsync(Guid id);
    Task<StudentDetail?> CreateAsync(StudentDetail studentDetail);
    Task<StudentDetail?> UpdateAsync(Guid id, StudentDetail studentDetail);
    Task<StudentDetail?> DeleteAsync(Guid id);
}
