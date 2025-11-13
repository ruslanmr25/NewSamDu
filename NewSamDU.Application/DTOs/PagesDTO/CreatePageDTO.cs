using System;
using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.PagesDTO
{
    public class CreatePageDTO
    {
        [Required(ErrorMessage = "Sahifa nomi (lotincha - Uz) kiritilishi shart")]
        [MaxLength(200, ErrorMessage = "Sahifa nomi 200 ta belgidan oshmasligi kerak")]
        public string TitleUz { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sahifa nomi (inglizcha - En) kiritilishi shart")]
        [MaxLength(200, ErrorMessage = "Sahifa nomi 200 ta belgidan oshmasligi kerak")]
        public string TitleEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sahifa nomi (ruscha - Ru) kiritilishi shart")]
        [MaxLength(200, ErrorMessage = "Sahifa nomi 200 ta belgidan oshmasligi kerak")]
        public string TitleRu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sahifa nomi (kirillcha - Kr) kiritilishi shart")]
        [MaxLength(200, ErrorMessage = "Sahifa nomi 200 ta belgidan oshmasligi kerak")]
        public string TitleKr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kontent (lotincha - Uz) kiritilishi shart")]
        public string ContentUz { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kontent (ruscha - Ru) kiritilishi shart")]
        public string ContentRu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kontent (inglizcha - En) kiritilishi shart")]
        public string ContentEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kontent (kirillcha - Kr) kiritilishi shart")]
        public string ContentKr { get; set; } = string.Empty;
    }
}
