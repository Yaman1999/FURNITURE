using System;
using System.Collections.Generic;

#nullable disable

namespace FURNITURE.Models
{
    public partial class RoleZ
    {
        public RoleZ()
        {
            LoginZs = new HashSet<LoginZ>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LoginZ> LoginZs { get; set; }
    }
}
