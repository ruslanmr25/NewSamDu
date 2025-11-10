using System;
using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.NewsDTOs
{
    public class CreateNewsDTO
    {
        [Required(ErrorMessage = "Title (Uz) kiritilishi shart.")]
        [StringLength(200, ErrorMessage = "Title (Uz) 200 ta belgidan oshmasligi kerak.")]
        public string TitleUz { get; set; } = string.Empty;

        [Required(ErrorMessage = "Title (En) kiritilishi shart.")]
        [StringLength(200, ErrorMessage = "Title (En) 200 ta belgidan oshmasligi kerak.")]
        public string TitleEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Title (Ru) kiritilishi shart.")]
        [StringLength(200, ErrorMessage = "Title (Ru) 200 ta belgidan oshmasligi kerak.")]
        public string TitleRu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Title (Krill) kiritilishi shart.")]
        [StringLength(200, ErrorMessage = "Title (Krill) 200 ta belgidan oshmasligi kerak.")]
        public string TitleKr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description (Uz) kiritilishi shart.")]
        [StringLength(1000, ErrorMessage = "Description (Uz) 1000 ta belgidan oshmasligi kerak.")]
        public string DescriptionUz { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description (Ru) kiritilishi shart.")]
        [StringLength(1000, ErrorMessage = "Description (Ru) 1000 ta belgidan oshmasligi kerak.")]
        public string DescriptionRu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description (En) kiritilishi shart.")]
        [StringLength(1000, ErrorMessage = "Description (En) 1000 ta belgidan oshmasligi kerak.")]
        public string DescriptionEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description (Krill) kiritilishi shart.")]
        [StringLength(
            1000,
            ErrorMessage = "Description (Krill) 1000 ta belgidan oshmasligi kerak."
        )]
        public string DescriptionKr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content (Uz) kiritilishi shart.")]
        public string ContentUz { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content (Ru) kiritilishi shart.")]
        public string ContentRu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content (En) kiritilishi shart.")]
        public string ContentEn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content (Krill) kiritilishi shart.")]
        public string ContentKr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Asosiy rasm yo‘li (MainImagePath) kiritilishi shart.")]
        //    [Url(ErrorMessage = "MainImagePath noto‘g‘ri URL formatida.")]
        public string MainImagePath { get; set; } = string.Empty;
    }
}
