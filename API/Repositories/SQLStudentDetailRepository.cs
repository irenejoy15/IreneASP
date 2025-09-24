using System;
using API.Data;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class SQLStudentDetailRepository : IStudentDetailRepository
{
    private readonly IreneDBContext dBContext;

    public SQLStudentDetailRepository(IreneDBContext dBContext)
    {
        this.dBContext = dBContext;
    }

    public async Task<StudentDetail?> GetByIdAsync(Guid id)
    {
        return await dBContext.StudentDetails.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<StudentDetail?> CreateAsync(StudentDetail studentDetail)
    {
        await dBContext.StudentDetails.AddAsync(studentDetail);
        await dBContext.SaveChangesAsync();
        return studentDetail;
    }

    public async Task<StudentDetail?> UpdateAsync(Guid id, StudentDetail studentDetail)
    {
        var existingStudentDetail = await dBContext.StudentDetails.FirstOrDefaultAsync(r => r.Id == id);
        if (existingStudentDetail == null)
        {
            return null;
        }
        existingStudentDetail.Id = studentDetail.Id;
        await dBContext.SaveChangesAsync();
        return existingStudentDetail;
    }

    public async Task<StudentDetail?> DeleteAsync(Guid id)
    {
        var existingStudentDetail = await dBContext.StudentDetails.FirstOrDefaultAsync(r => r.Id == id);
        if (existingStudentDetail == null)
        {
            return null;
        }
        dBContext.StudentDetails.Remove(existingStudentDetail);
        await dBContext.SaveChangesAsync();
        return existingStudentDetail;
    }
}
