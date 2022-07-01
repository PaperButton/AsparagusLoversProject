using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsparagusLoversProject.Domain
{
    public class FoodIntakeCounter : IFoodIntakeCounter
    {
        [Key]
        public Guid RecordId { get; set; }
        public Guid LoverID { get; set; }
        public int NumberOfMealsOfFood { get; set; }
        public DateTime LastFoodEatenDateTime { get; set; }
      
        public ILover Lover { get; set;}
     
    }
}
