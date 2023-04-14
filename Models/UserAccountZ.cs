using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FURNITURE.Models
{
    public partial class UserAccountZ
    {
        public UserAccountZ()
        {
            LoginZs = new HashSet<LoginZ>();
            OrderZs = new HashSet<OrderZ>();
            PaymentZs = new HashSet<PaymentZ>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal Id { get; set; }
        public string Fullname { get; set; }
        public decimal? Phone { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Email { get; set; }

        public virtual ICollection<LoginZ> LoginZs { get; set; }
        public virtual ICollection<OrderZ> OrderZs { get; set; }
        public virtual ICollection<PaymentZ> PaymentZs { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
