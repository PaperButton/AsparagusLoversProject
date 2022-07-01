using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsparagusLoversProject.Domain
{
    public interface ILover
    {
        /// <summary>
        /// Record ID in the database
        /// </summary>
        [Required]
        [Key]
        public Guid LoverID { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        [Required(ErrorMessage = "Please enter a name")]
        public string Fname { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        [Required(ErrorMessage = "Please enter an email")]
        public string EMail { get; set; }


        
        public IFoodIntakeCounter FoodIntakeCounter { get; set; }

    }
}
