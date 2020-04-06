using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models.ViewModels
{
    public class DetailsAllExpenseAndYield
    {
        public IEnumerable<TotalPorCategoriaFormViewModel> TotalPorCategoriaFixa { get; set; }
        public IEnumerable<TotalPorCategoriaFormViewModel> TotalPorCategoriaVariavel { get; set; }
        public IEnumerable<TotalPorCategoriaFormViewModel> TotalPorCategoriaExtra { get; set; }
        public IEnumerable<Receita> Receitas { get; set; }
    }
}
