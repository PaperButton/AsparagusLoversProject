using System;
using System.Collections.Generic;

namespace AsparagusLoversProject.Models
{
    public partial class Lover
    {
        public Guid LoverId { get; set; }
        public string Fname { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual FoodIntakeCounter FoodIntakeCounter { get; set; } = null!;
    }
}
