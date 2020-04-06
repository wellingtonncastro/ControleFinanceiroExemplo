using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels;
using ControleFinanceiro.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Repository
{
    public class AnothersExpenseRepository : IAnothersExpenseRepository
    {
        private ControleFinanceiroContext _controleFinanceiroContext;
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<User> _userManager;

        public AnothersExpenseRepository(UserManager<User> userManager,ICategoryRepository categoryRepository,ControleFinanceiroContext controleFinanceiroContext)
        {
            _controleFinanceiroContext = controleFinanceiroContext;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }

        public async Task<List<Despesa>> FindAllExpenseByUserAndTypeAsync(string id, int typeId)
        {
            return await _controleFinanceiroContext.Despesas.Where(user => user.UserId == id).Where(type => type.TipoId == typeId).Include(expense => expense.Categoria).Include(expense => expense.Tipo).ToListAsync();
        }

        public async Task RegisterNewExpenseAsync(Despesa despesa)
        {
            _controleFinanceiroContext.Despesas.Add(despesa);
            await _controleFinanceiroContext.SaveChangesAsync();
        }

        public async Task<Despesa> FindExpenseByIdAsync(int id)
        {
            return await _controleFinanceiroContext.Despesas.FindAsync(id);
        }

        public async Task<List<TotalPorCategoriaFormViewModel>> GetTotalCategoryPerUserIdAnTypeIdAsync(string userId, int typeId)
        {
            var expense = await _controleFinanceiroContext.Despesas.Where(user => user.UserId == userId).Where(type =>type.TipoId == typeId).ToListAsync();
            var category = await _categoryRepository.FindAll();

            List<TotalPorCategoriaFormViewModel> totalPorCategoriaForm = new List<TotalPorCategoriaFormViewModel>();

            foreach (var item in category)
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


        public async Task UpdateExpenseAync(Despesa despesa)
        {
            try
            {

                _controleFinanceiroContext.Despesas.Update(despesa);
                await _controleFinanceiroContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {

                throw new DbUpdateConcurrencyException(e.Message);
            }
        }

        public async Task DeleteExpenseAync(int id)
        {
            try
            {
                var despesa = await FindExpenseByIdAsync(id);
                _controleFinanceiroContext.Despesas.Remove(despesa);
                await _controleFinanceiroContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {

                throw new DbUpdateException(e.Message);
            }

        }

        public async Task DeleteAllExtraExpenseAsync(string userId)
        {
            _controleFinanceiroContext.Despesas.RemoveRange(_controleFinanceiroContext.Despesas.Where(tipo => tipo.TipoId == 3).Where(user => user.UserId == userId));
            await _controleFinanceiroContext.SaveChangesAsync();
        }

        public async Task UpdateAllVariableExpenseAsync(string userId) 
        {
            var despesas = await FindAllExpenseByUserAndTypeAsync(userId, 2);

            foreach(var item in despesas) 
            {
                item.Valor = 0;
            }

            _controleFinanceiroContext.Despesas.UpdateRange(despesas);
            await _controleFinanceiroContext.SaveChangesAsync();
        }

        public async Task VerifyDateForDeleteDataAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (DateTime.Now.Day == 4 && !user.DespesasExcluidas)
            {
                try
                {
                    await DeleteAllExtraExpenseAsync(id);
                    await UpdateAllVariableExpenseAsync(id);

                    user.DespesasExcluidas = true;
                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException(e.Message);
                }
            }
            else
            {
                if (DateTime.Now.Day != 4 && user.DespesasExcluidas)
                {
                    user.DespesasExcluidas = false;
                    await _userManager.UpdateAsync(user);
                
                }
            }  
        }
    }
}
