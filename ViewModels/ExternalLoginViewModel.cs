using System.ComponentModel.DataAnnotations;

namespace AsparagusLoversProject.ViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}