using System;
using API.Data;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class SQLStudentRepository : IStudentRepository
{
    private readonly IreneDBContext dBContext;

    public SQLStudentRepository(IreneDBContext dBContext)
    {
        this.dBContext = dBContext;
    }

    public async Task<StudentHeader> CreateAsync(StudentHeader studentHeader)
    {
        await dBContext.StudentHeaders.AddAsync(studentHeader);
        var details = studentHeader.StudentDetails;
        if (details != null && details.Any())
        {
            foreach (var detail in details)
            {
                detail.StudentHeaderId = studentHeader.Id;
                await dBContext.StudentDetails.AddAsync(detail);
            }
        }
        await dBContext.SaveChangesAsync();
        return studentHeader;
    }

    public async Task<StudentHeader?> DeleteAsync(Guid id)
    {
        var existingStudentHeader = await dBContext.StudentHeaders.FirstOrDefaultAsync(r => r.Id == id);
        if (existingStudentHeader == null)
        {
            return null;
        }
        dBContext.StudentHeaders.Remove(existingStudentHeader);
        await dBContext.SaveChangesAsync();
        return existingStudentHeader;
    }

    public async Task<List<StudentHeader>> GetAllAsync()
    {
        return await dBContext.StudentHeaders.
        Include(sh => sh.StudentDetails).
            ThenInclude(sd => sd.Subject).
            Include(sh => sh.StudentDetails).ThenInclude(sd => sd.Teacher).
            ToListAsync();
    }

    public async Task<StudentHeader?> GetByIdAsync(Guid id)
    {
        return await dBContext.StudentHeaders.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<StudentHeader?> UpdateAsync(Guid id, StudentHeader studentHeader)
    {
        var existingStudentHeader = await dBContext.StudentHeaders.FirstOrDefaultAsync(r => r.Id == id);
        if (existingStudentHeader == null)
        {
            return null;
        }
        existingStudentHeader.Name = studentHeader.Name;
        await dBContext.SaveChangesAsync();
        return existingStudentHeader;
    }

}
