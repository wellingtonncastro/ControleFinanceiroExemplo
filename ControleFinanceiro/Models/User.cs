using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models
{
    public class User:IdentityUser
    {
        [Required(ErrorMessage = "{0} required")]
        public int QuantidadeMembroFamilia { get; set; }
        public bool DespesasExcluidas { get; set; }

    }
}
