using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Interfaces
{
    public interface IExpenseRepository
    {
        Task<List<DespesaFixa>> FindAllExpenseFixedByUserAsync(string id);
        Task RegisterNewExpenseFixedAsync(DespesaFixa despesaFixa);
        Task<DespesaFixa> FindExpenseFixedByIdAsync(int id);
        Task UpdateExpenseFixedAync(DespesaFixa despesaFixa);
        Task DeleteExpenseFixedAync(int id);
        Task<List<TotalPorCategoriaFormViewModel>> GetTotalCategoryPerUserIdAsync(string userId);
        
    }
}
