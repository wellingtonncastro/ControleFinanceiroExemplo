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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ControleFinanceiroContext _controleFinanceiroContext;
        public CategoryRepository(ControleFinanceiroContext controleFinanceiroContext)
        {
            _controleFinanceiroContext = controleFinanceiroContext;
        }

        public async Task<List<Categoria>> FindAll() 
        {
            return await _controleFinanceiroContext.Categorias.ToListAsync();
        }

        public async Task<Categoria> FindByIdAsync(int id) 
        {
            return await _controleFinanceiroContext.Categorias.FindAsync(id);
        }
    }
}
