using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models.ViewModels
{
    public class TotalPorCategoriaFormViewModel
    {
        public Categoria Categorias { get; set; }
        public int CategoriaId { get; set; }
        public  decimal Total { get; set; }
    }
}
