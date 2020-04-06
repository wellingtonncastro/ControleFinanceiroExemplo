using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string NomeCategoria { get; set; }

        public Categoria(int id, string nomeCategoria )
        {
            Id = id;
            NomeCategoria = nomeCategoria;
        }
    }
}
