using System.ComponentModel.DataAnnotations;

namespace CIS174WebApp.Models.AccountViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
