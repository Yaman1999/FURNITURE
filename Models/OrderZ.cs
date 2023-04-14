using System;
using System.Collections.Generic;

#nullable disable

namespace FURNITURE.Models
{
    public partial class OrderZ
    {
        public OrderZ()
        {
            ProductOrderZs = new HashSet<ProductOrderZ>();
        }

        public decimal Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal? UserId { get; set; }

        public virtual UserAccountZ User { get; set; }
        public virtual ICollection<ProductOrderZ> ProductOrderZs { get; set; }
    }
}
