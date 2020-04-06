using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Interfaces
{
    public interface IAnothersExpenseRepository
    {
        Task<List<Despesa>> FindAllExpenseByUserAndTypeAsync(string id, int typeId);
        Task RegisterNewExpenseAsync(Despesa despesa);
        Task<Despesa> FindExpenseByIdAsync(int id);
        Task<List<TotalPorCategoriaFormViewModel>> GetTotalCategoryPerUserIdAnTypeIdAsync(string userId, int typeId);
        Task UpdateExpenseAync(Despesa despesa);
        Task DeleteExpenseAync(int id);
        Task DeleteAllExtraExpenseAsync(string userId);
        Task UpdateAllVariableExpenseAsync(string userId);
        Task VerifyDateForDeleteDataAsync(string id);
    }
}
