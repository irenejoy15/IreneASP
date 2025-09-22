using System;
using API.Models.Domain;

namespace API.Repositories;

public interface ITeacherRepository
{
    Task<List<Teacher>> GetAllAsync();
    Task<Teacher?> GetByIdAsync(Guid id);
    Task<Teacher?> CreateAsync(Teacher teacher);
    Task<Teacher?> UpdateAsync(Guid id, Teacher teacher);
    Task<Teacher?> DeleteAsync(Guid id);
}
