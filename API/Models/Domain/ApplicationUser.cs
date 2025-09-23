using System;
using Microsoft.AspNetCore.Identity;

namespace API.Models.Domain;

public class ApplicationUser:IdentityUser
{
    public string? Signature { get; set; }
    public bool IsActive { get; set; } = true;
}
