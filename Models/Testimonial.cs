using System;
using System.Collections.Generic;

#nullable disable

namespace FURNITURE.Models
{
    public partial class Testimonial
    {
        public decimal Id { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public decimal? UserId { get; set; }

        public virtual UserAccountZ User { get; set; }
    }
}
