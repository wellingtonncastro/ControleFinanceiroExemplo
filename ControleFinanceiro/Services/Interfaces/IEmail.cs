using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Services.Interfaces
{
    public interface IEmail
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
