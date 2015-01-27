using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class GeneratedPdf
    {
        [Key]
        [Required]
        public string Reference { get; set; }
        public byte[] Content { get; set; }

    }
}