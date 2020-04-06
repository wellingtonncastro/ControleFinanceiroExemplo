using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models.ViewModels.AnothersExpenses
{
    public class DetailsAnothersExpenseFormViewModel
    {
        public IEnumerable<Despesa> Despesas{ get; set; }
        public IEnumerable<TotalPorCategoriaFormViewModel> TotalPorCategoria { get; set; }
    }
}
