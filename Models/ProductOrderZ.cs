using System;
using System.Collections.Generic;

#nullable disable

namespace FURNITURE.Models
{
    public partial class ProductOrderZ
    {
        public decimal Id { get; set; }
        public string Status { get; set; }
        public decimal? ProductId { get; set; }
        public decimal? OrderId { get; set; }
        public decimal? Quantity { get; set; }

        public virtual OrderZ Order { get; set; }
        public virtual ProductZ Product { get; set; }
    }
}
