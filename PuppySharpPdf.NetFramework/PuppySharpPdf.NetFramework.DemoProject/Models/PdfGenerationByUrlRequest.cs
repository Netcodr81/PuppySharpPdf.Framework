﻿using System.ComponentModel.DataAnnotations;

namespace PuppySharpPdf.NetFramework.DemoProject.Models
{
    public class PdfGenerationByUrlRequest
    {
        [Required(ErrorMessage = "A url is required")]
        public string Url { get; set; }
        public bool UseLocalExe { get; set; }
        public PdfOptionsViewModel PdfOptions { get; set; }
    }
}
