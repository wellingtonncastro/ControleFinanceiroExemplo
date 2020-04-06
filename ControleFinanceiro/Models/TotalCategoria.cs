using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models
{
    public class TotalCategoria
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
        public decimal Total { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}

