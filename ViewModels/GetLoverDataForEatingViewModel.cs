using System.ComponentModel.DataAnnotations;

namespace AsparagusLoversProject.ViewModels
{
    public class GetLoverDataForEatingViewModel
    {
        /// <summary>
        /// User's first name
        /// </summary>
        [Required]
        public string LoverFname { get; set; } = string.Empty;

        /// <summary>
        /// User's email
        /// </summary>
        [Required]
        public string LoverEMail { get; set; } = string.Empty;
    }
}
