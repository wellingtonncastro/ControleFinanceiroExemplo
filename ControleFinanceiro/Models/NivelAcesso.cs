using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models
{
    public class NivelAcesso : IdentityRole
    {
        public string descricao { get; set; }
    }
}
