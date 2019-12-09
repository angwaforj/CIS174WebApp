using System.ComponentModel.DataAnnotations;

namespace CIS174WebApp.Models.AccountViewModel
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
