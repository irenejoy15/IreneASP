using System;

namespace API.Models.DTO;
using System.ComponentModel.DataAnnotations;
public class RegisterRequestDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public required string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    public required string[] Roles { get; set; }

    public string? Signature { get; set; }

    public bool IsActive { get; set; } = true;
}
