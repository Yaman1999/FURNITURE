using System;
using System.Collections.Generic;

#nullable disable

namespace FURNITURE.Models
{
    public partial class LoginZ
    {
        public decimal Id { get; set; }
        public string UserName { get; set; }
        public string Passwordd { get; set; }
        public decimal? UserId { get; set; }
        public decimal? RoleId { get; set; }

        public virtual RoleZ Role { get; set; }
        public virtual UserAccountZ User { get; set; }
    }
}
