using System.ComponentModel.DataAnnotations;

namespace AsparagusLoversProject.Domain
{
    public interface IFoodIntakeCounter
    {
        [Key]
        /// <summary>
        /// This property is needed in order to be able to use the FoodIntakeCounter navigation in the entity framework core
        /// </summary>
        public Guid RecordId { get; set; }

        /// <summary>
        /// It's the foreign key
        /// </summary>
        public Guid LoverID { get; set; }
        /// <summary>
        /// User's number of meals taken
        /// </summary>
        public int NumberOfMealsOfFood { get; set; }

        /// <summary>
        /// Date and time of the last food eaten
        /// </summary>
        public DateTime LastFoodEatenDateTime { get; set; }

        /// <summary>
        /// The property to access the foreign key
        /// </summary>
        public ILover Lover { get; set; }

    }
}
