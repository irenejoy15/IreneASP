using System;
using API.Models.Domain;

namespace API.Repositories;

public interface ISubjectRepository
{
    Task<List<Subject>> GetAllAsync();
    Task<Subject?> GetByIdAsync(Guid id);
    Task<Subject?> CreateAsync(Subject subject);
    Task<Subject?> UpdateAsync(Guid id, Subject subject);
    Task<Subject?> DeleteAsync(Guid id);

}
