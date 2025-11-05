using System;
using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.AuthDTOs;

public class LoginDTO
{
    [Required]
    [MaxLength(255)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;
}
