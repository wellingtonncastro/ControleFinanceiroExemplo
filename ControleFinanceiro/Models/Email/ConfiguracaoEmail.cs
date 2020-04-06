using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models.Email
{
    public class ConfiguracaoEmail
    {
        public string Endereco { get; set; }
        public int Porta { get; set; }
        public string Email { get; set; }
        public string senhaAcesso { get; set; }
        public string Destino { get; set; }
    }
}
