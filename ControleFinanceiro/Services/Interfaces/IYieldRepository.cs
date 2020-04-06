using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Interfaces
{
    public interface IYieldRepository
    {
        Task<List<Receita>> FindAllByUserAsync(string id);
        Task RegisterNewYieldAsync(Receita receita);
        Task<Receita> FindYieldByIdAsync(int id);
        Task UpdateYieldAsync(Receita yield);
        Task DeleteYieldAsync(int id);
    }
}
