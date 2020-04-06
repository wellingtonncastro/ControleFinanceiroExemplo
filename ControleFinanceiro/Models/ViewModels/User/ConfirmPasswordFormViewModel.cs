using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models.ViewModels.User
{
    public class ConfirmPasswordFormViewModel
    {
        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long .", MinimumLength = 8)]
        [RegularExpression(@"^.*(?=.{8,})(?=.*[@#$%^&+=])(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "The{0} must be numbers, special characters and upper case letters")]
        [Display(Name = "Senha atual")]
        public string CurrencyPasswordHash { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long .", MinimumLength = 8)]
        [RegularExpression(@"^.*(?=.{8,})(?=.*[@#$%^&+=])(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "The{0} must be numbers, special characters and upper case letters")]
        [Display(Name = "Nova senha")]
        public string NewPasswordHash { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long, must be special character and one Upper Latter .", MinimumLength = 8)]
        [Compare("NewPasswordHash", ErrorMessage = "The password and confirm password do not match")]
        [Display(Name = "Confirme a nova senha")]
        public string ConfirmPasswordHash { get; set; }
    }
}
