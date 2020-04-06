using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models
{
    public class Mes
    {
        public int Id { get; set; }
        public string NomeMes { get; set; }

        public Mes(int id, string nomeMes)
        {
            Id = id;
            NomeMes = nomeMes;
        }
    }
}
