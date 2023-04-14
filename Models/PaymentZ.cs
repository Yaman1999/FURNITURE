using System;
using System.Collections.Generic;

#nullable disable

namespace FURNITURE.Models
{
    public partial class PaymentZ
    {
        public decimal Id { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PayDate { get; set; }
        public decimal? UserId { get; set; }

        public virtual UserAccountZ User { get; set; }
    }
}
