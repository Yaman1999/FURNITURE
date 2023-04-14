using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FURNITURE.Models
{
    public partial class HomePageZ
    {
        public decimal Id { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Logo { get; set; }
        [NotMapped]
        public IFormFile ImageLogo { get; set; }
        public string Paragraph { get; set; }
        public string Email { get; set; }
        public decimal? Phone { get; set; }
        public string Address { get; set; }
        public string Text1 { get; set; }
        public string Image2 { get; set; }
        [NotMapped]
        public IFormFile ImageFile2 { get; set; }
    }
}
