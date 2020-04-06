using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models
{
    public class Tipo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public Tipo(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
