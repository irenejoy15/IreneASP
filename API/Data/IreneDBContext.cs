using System;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class IreneDBContext : DbContext
{
    public IreneDBContext(DbContextOptions<IreneDBContext> options) : base(options)
    {
    }

    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // var Subjects = new List<Subject>
        // {
        //     new Subject { Id = Guid.Parse("2f6692aa-5d98-4bba-8a34-c4da0cf5f0a7"), SubjectName = "Mathematics", SubjectCode = "MATH101", Description = "Basic Mathematics"  },
        // };
        // modelBuilder.Entity<Subject>().HasData(Subjects);

    }
}
