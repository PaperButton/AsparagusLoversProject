using System.ComponentModel.DataAnnotations;


namespace AsparagusLoversProject.Domain
{
    public class AuthenticationProviderrr
    {
        [Key]
        public int AuthenticationProviderrrID { get; set; }
        [Required]
        public string ProviderrrName { get; set; }
        //public List<ILover> Lover { get; set; }
    }
}
