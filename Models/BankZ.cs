using System;
using System.Collections.Generic;

#nullable disable

namespace FURNITURE.Models
{
    public partial class BankZ
    {
        public decimal Id { get; set; }
        public decimal? CardNumber { get; set; }
        public decimal? Cvv { get; set; }
        public decimal? Amount { get; set; }
    }
}
