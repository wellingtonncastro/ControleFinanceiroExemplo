using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Interfaces
{
    public interface ITypeRepository
    {
        Task<Tipo> FindTypeById(int id);
    }
}
