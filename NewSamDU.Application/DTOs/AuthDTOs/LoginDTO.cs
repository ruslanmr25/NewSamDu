using System;
using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.AuthDTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "Foydalanuvchi nomi kiritilmagan")]
    [MaxLength(255)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parol kiritilmagan")]
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;
}
