using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FURNITURE.Models
{
    public partial class CategoryZ
    {
        public CategoryZ()
        {
            ProductZs = new HashSet<ProductZ>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual ICollection<ProductZ> ProductZs { get; set; }
    }
}
