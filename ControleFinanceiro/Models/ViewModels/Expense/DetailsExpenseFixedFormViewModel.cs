using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models.ViewModels.Expense
{
    public class DetailsExpenseFixedFormViewModel
    {
        public IEnumerable<DespesaFixa> DespesaFixa { get; set; }
        public IEnumerable<TotalPorCategoriaFormViewModel> TotalPorCategoria { get; set; }

    }
}
