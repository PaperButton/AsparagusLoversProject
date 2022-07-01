using System;
using System.Collections.Generic;

namespace AsparagusLoversProject.Models
{
    public partial class FoodIntakeCounter
    {
        public Guid RecordId { get; set; }
        public Guid LoverId { get; set; }
        public int NumberOfMealsOfFood { get; set; }
        public DateTime LastFoodEatenDateTime { get; set; }

        public virtual Lover Lover { get; set; } = null!;
    }
}
