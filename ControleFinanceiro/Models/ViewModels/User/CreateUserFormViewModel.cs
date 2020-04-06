using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models.ViewModels.User
{
    public class CreateUserFormViewModel
    {
        [Required(ErrorMessage = "{0} required")]
        [Remote(action: "VerifyUserName", controller: "Users")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long .")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Users")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long .", MinimumLength = 8)]
        [RegularExpression(@"^.*(?=.{8,})(?=.*[@#$%^&+=])(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "The{0} must be numbers, special characters and upper case letters")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long .", MinimumLength = 8)]
        [RegularExpression(@"^.*(?=.{8,})(?=.*[@#$%^&+=])(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "The{0} must be numbers, special characters and upper case letters")]
        [Compare("PasswordHash", ErrorMessage = "The password and confirm password do not match")]
        public string ConfirmPasswordHash { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public int QuantidadeMembroFamilia { get; set; }
    }
}
