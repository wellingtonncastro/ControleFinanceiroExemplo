using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using ControleFinanceiro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Repository
{
    public class YieldRepository : IYieldRepository
    {
        private ControleFinanceiroContext _controleFinanceiroContext;
        public YieldRepository(ControleFinanceiroContext controleFinanceiroContext)
        {
            _controleFinanceiroContext = controleFinanceiroContext;
        }

        public async Task<List<Receita>> FindAllByUserAsync(string id) 
        {
            return await _controleFinanceiroContext.Receitas.Where(user => user.UserId == id).ToListAsync();
        }

        public async Task RegisterNewYieldAsync(Receita receita) 
        {
            _controleFinanceiroContext.Receitas.Add(receita);
            await _controleFinanceiroContext.SaveChangesAsync();
        }

        public async Task<Receita> FindYieldByIdAsync(int id) 
        {
           return await _controleFinanceiroContext.Receitas.FindAsync(id);
        }

        public async Task UpdateYieldAsync(Receita yield)
        {
            try
            {
                _controleFinanceiroContext.Receitas.Update(yield);
                await _controleFinanceiroContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {

                throw new DbUpdateConcurrencyException(e.Message);
            }
        }

        public async Task DeleteYieldAsync(int id) 
        {
            try
            {
                var usuario = await FindYieldByIdAsync(id);

                _controleFinanceiroContext.Receitas.Remove(usuario);
                await _controleFinanceiroContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }
    }
}
