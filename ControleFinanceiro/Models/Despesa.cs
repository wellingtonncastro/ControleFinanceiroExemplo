using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
        public Tipo Tipo { get; set; }
        public int TipoId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public DateTime Data { get; set; }

    }
}
