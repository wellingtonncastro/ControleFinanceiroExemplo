using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models.ViewModels.User
{
    public class ResetPasswordFormViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long, must be special character and one Upper Latter .", MinimumLength = 8)]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long, must be special character and one Upper Latter .", MinimumLength = 8)]
        [Compare("PasswordHash", ErrorMessage = "The password and confirm password do not match")]
        public string ConfirmPasswordHash { get; set; }

        public string Token { get; set; }
    }
}
