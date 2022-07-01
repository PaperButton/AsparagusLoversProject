using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsparagusLoversProject.Domain
{
    public class AsparagusLover : ILover
    {
        [Key]
        public Guid LoverID { get; set; }
        public string Fname { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;

        
        public IFoodIntakeCounter FoodIntakeCounter { get; set; }
    }
}
