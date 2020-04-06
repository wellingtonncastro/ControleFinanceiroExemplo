using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Categoria>> FindAll();
        Task<Categoria> FindByIdAsync(int id);
    }
}
