using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using ControleFinanceiro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Repository
{
    public class TypeRepository : ITypeRepository
    {
        private ControleFinanceiroContext _controleFinanceiroContext;

        public TypeRepository(ControleFinanceiroContext controleFinanceiroContext)
        {
            _controleFinanceiroContext = controleFinanceiroContext;
        }

        public async Task<Tipo> FindTypeById(int id) 
        {
            return await _controleFinanceiroContext.Tipos.FindAsync(id);
        }
    }
}
