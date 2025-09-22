using System;
using API.Data;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class SQLSubjectRepository : ISubjectRepository
{
    private readonly IreneDBContext dBContext;
    public SQLSubjectRepository(IreneDBContext dBContext)
    {
        this.dBContext = dBContext;
    }

    // Implement CRUD operations here
    public async Task<List<Subject>> GetAllAsync()
    {
        return await dBContext.Subjects.ToListAsync();
    }

    public async Task<Subject?> GetByIdAsync(Guid id)
    {
        return await dBContext.Subjects.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Subject?> CreateAsync(Subject subject)
    {   
        await dBContext.Subjects.AddAsync(subject);
        //  var irene = await dBContext.Subjects.AddAsync(subject);
        // irene.Entity.Id = Guid.NewGuid();
        // irene.Entity.SubjectName = subject.SubjectName;
        // irene.Entity.SubjectCode = subject.SubjectCode;
        // irene.Entity.Description = subject.Description;
        await dBContext.SaveChangesAsync();
        return subject;
    }

    public async Task<Subject?> UpdateAsync(Guid id, Subject subject)
    {
        var existingSubject = await dBContext.Subjects.FirstOrDefaultAsync(r => r.Id == id);
        if (existingSubject == null)
        {
            return null;
        }
        existingSubject.SubjectName = subject.SubjectName;
        existingSubject.SubjectCode = subject.SubjectCode;
        existingSubject.Description = subject.Description;

        await dBContext.SaveChangesAsync();
        return existingSubject;
    }

    public async Task<Subject?> DeleteAsync(Guid id)
    {
        var existingSubject = await dBContext.Subjects.FirstOrDefaultAsync(r => r.Id == id);
        if (existingSubject == null)
        {
            return null;
        }
        dBContext.Subjects.Remove(existingSubject);
        await dBContext.SaveChangesAsync();
        return existingSubject;
    }

}
