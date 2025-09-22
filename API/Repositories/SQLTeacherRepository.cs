using System;
using API.Data;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class SQLTeacherRepository : ITeacherRepository
{
    private readonly IreneDBContext dBContext;

    public SQLTeacherRepository(IreneDBContext dBContext)
    {
        this.dBContext = dBContext;
    }

    public async Task<List<Teacher>> GetAllAsync()
    {
        return await dBContext.Teachers.ToListAsync();
    }

    public async Task<Teacher?> GetByIdAsync(Guid id)
    {
        return await dBContext.Teachers.FindAsync(id);
    }

    public async Task<Teacher?> CreateAsync(Teacher teacher)
    {
        var createdEntity = await dBContext.Teachers.AddAsync(teacher);
        await dBContext.SaveChangesAsync();
        return createdEntity.Entity;
    }

    public async Task<Teacher?> UpdateAsync(Guid id, Teacher teacher)
    {
        var existingEntity = await dBContext.Teachers.FindAsync(id);
        if (existingEntity == null) return null;

        existingEntity.TeacherName = teacher.TeacherName;
        await dBContext.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<Teacher?> DeleteAsync(Guid id)
    {
        var existingEntity = await dBContext.Teachers.FindAsync(id);
        if (existingEntity == null) return null;

        dBContext.Teachers.Remove(existingEntity);
        await dBContext.SaveChangesAsync();
        return existingEntity;
    }
}
