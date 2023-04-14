using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FURNITURE.Models
{
    public partial class ProductZ
    {
        public ProductZ()
        {
            ProductOrderZs = new HashSet<ProductOrderZ>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public decimal? Price { get; set; }
        public decimal? Value { get; set; }
        public decimal? CategoryId { get; set; }

        public virtual CategoryZ Category { get; set; }
        public virtual ICollection<ProductOrderZ> ProductOrderZs { get; set; }
    }
}
