using System;
using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.PagesDTO;

public class UpdatePageDTO
{
    //     [Required(ErrorMessage = "Sahifa nomi (lotincha - Uz) kiritilishi shart")]
    [MaxLength(200, ErrorMessage = "Sahifa nomi 200 ta belgidan oshmasligi kerak")]
    public string? TitleUz { get; set; }

    //     [Required(ErrorMessage = "Sahifa nomi (inglizcha - En) kiritilishi shart")]
    [MaxLength(200, ErrorMessage = "Sahifa nomi 200 ta belgidan oshmasligi kerak")]
    public string? TitleEn { get; set; }

    //     [Required(ErrorMessage = "Sahifa nomi (ruscha - Ru) kiritilishi shart")]
    [MaxLength(200, ErrorMessage = "Sahifa nomi 200 ta belgidan oshmasligi kerak")]
    public string? TitleRu { get; set; }

    //     [Required(ErrorMessage = "Sahifa nomi (kirillcha - Kr) kiritilishi shart")]
    [MaxLength(200, ErrorMessage = "Sahifa nomi 200 ta belgidan oshmasligi kerak")]
    public string? TitleKr { get; set; }

    //     [Required(ErrorMessage = "Kontent (lotincha - Uz) kiritilishi shart")]
    public string? ContentUz { get; set; }

    //     [Required(ErrorMessage = "Kontent (ruscha - Ru) kiritilishi shart")]
    public string? ContentRu { get; set; }

    //     [Required(ErrorMessage = "Kontent (inglizcha - En) kiritilishi shart")]
    public string? ContentEn { get; set; }

    //     [Required(ErrorMessage = "Kontent (kirillcha - Kr) kiritilishi shart")]
    public string? ContentKr { get; set; }
}
