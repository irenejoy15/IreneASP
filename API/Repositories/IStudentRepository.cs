using System;
using API.Models.Domain;

namespace API.Repositories;

public interface IStudentRepository
{
    Task<List<StudentHeader>> GetAllAsync();
    Task<StudentHeader?> GetByIdAsync(Guid id);
    Task<StudentHeader?> CreateAsync(StudentHeader studentHeader);
    Task<StudentHeader?> UpdateAsync(Guid id, StudentHeader studentHeader);
    Task<StudentHeader?> DeleteAsync(Guid id);
}
