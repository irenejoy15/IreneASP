using System;
using API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class IreneAuthDbContext : IdentityDbContext<ApplicationUser>
{
    public IreneAuthDbContext(DbContextOptions<IreneAuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var SuperAdminRoleId = "d2c1f3b4-5e6f-7a8b-9c0d-e1f2g3h4i5j6";
        var userRoleId = "a76a35de-117e-4da9-8161-f26db830a05b";

        var roles = new List<IdentityRole>
        {
            new IdentityRole { Id = SuperAdminRoleId, ConcurrencyStamp = SuperAdminRoleId, Name = "SuperAdmin", NormalizedName = "SUPERADMIN".ToUpper() },
            new IdentityRole { Id = userRoleId, ConcurrencyStamp = userRoleId, Name = "User", NormalizedName = "USER".ToUpper() }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}
