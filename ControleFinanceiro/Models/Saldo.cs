using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models
{
    public class Saldo
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }


    }
}
