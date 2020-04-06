using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels;
using ControleFinanceiro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private ControleFinanceiroContext _controleFinanceiroContext;
        private readonly ICategoryRepository _categoryRepository;
        public ExpenseRepository(ControleFinanceiroContext controleFinanceiroContext, ICategoryRepository categoryRepository)
        {
            _controleFinanceiroContext = controleFinanceiroContext;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<DespesaFixa>> FindAllExpenseFixedByUserAsync(string id) 
        {
            return await _controleFinanceiroContext.DespesaFixas.Where(user => user.UserId == id).Include(expense =>expense.Categoria)
                .ToListAsync();
        }

        public async Task RegisterNewExpenseFixedAsync(DespesaFixa despesaFixa)
        {
            _controleFinanceiroContext.DespesaFixas.Add(despesaFixa);
            await _controleFinanceiroContext.SaveChangesAsync();
        }

        public async Task<DespesaFixa> FindExpenseFixedByIdAsync(int id) 
        {
            return await _controleFinanceiroContext.DespesaFixas.FindAsync(id);
        }

            
        public async Task<List<TotalPorCategoriaFormViewModel>> GetTotalCategoryPerUserIdAsync(string userId) 
        {
            var expense = await _controleFinanceiroContext.DespesaFixas.Where(user => user.UserId == userId).ToListAsync();
            var category = await _categoryRepository.FindAll();

            List<TotalPorCategoriaFormViewModel> totalPorCategoriaForm = new List<TotalPorCategoriaFormViewModel>();
            
            foreach(var item in category) 
            {
                var totalCategoria = new TotalPorCategoriaFormViewModel() 
                {
                    Total = expense.Where(cat => cat.CategoriaId == item.Id).Sum(v => v.Valor),
                    Categorias = item,
                    CategoriaId = item.Id
                };
                
                totalPorCategoriaForm.Add(totalCategoria);
            }

            return totalPorCategoriaForm;
        }


        public async Task UpdateExpenseFixedAync(DespesaFixa despesaFixa) 
        {
            _controleFinanceiroContext.Update(despesaFixa);
            await _controleFinanceiroContext.SaveChangesAsync();
        }


        public async Task DeleteExpenseFixedAync(int id) 
        {
            try
            {
                var despesa = await FindExpenseFixedByIdAsync(id);
                _controleFinanceiroContext.DespesaFixas.Remove(despesa);
                await _controleFinanceiroContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {

                throw new DbUpdateException(e.Message);
            }

        }

    }
}
