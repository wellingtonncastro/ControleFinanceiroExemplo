using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Data
{
    public class SeedingServices
    {
        private ControleFinanceiroContext _controleFinanceiroContext;

        public SeedingServices(ControleFinanceiroContext controleFinanceiroContext)
        {
            _controleFinanceiroContext = controleFinanceiroContext;
        }

        public void Seed() 
        {
            if (_controleFinanceiroContext.Categorias.Any() || _controleFinanceiroContext.Meses.Any() || _controleFinanceiroContext.Meses.Any()) 
            {
                return;
            }
            else 
            {
                Mes mes01 = new Mes(1,"Janeiro");
                Mes mes02 = new Mes(2,"Fevereiro");
                Mes mes03 = new Mes(3,"Março");
                Mes mes04 = new Mes(4,"Abril");
                Mes mes05 = new Mes(5,"Maio");
                Mes mes06 = new Mes(6,"Junho");
                Mes mes07 = new Mes(7,"Julho");
                Mes mes08 = new Mes(8,"Agosto");
                Mes mes09 = new Mes(9,"Setembro");
                Mes mes10 = new Mes(10,"Outubro");
                Mes mes11 = new Mes(11,"Novembro");
                Mes mes12 = new Mes(12,"Dezembro");

                Categoria cat01 = new Categoria(1, "Habitação");
                Categoria cat02 = new Categoria(2, "Transporte");
                Categoria cat03 = new Categoria(3, "Saude");
                Categoria cat04 = new Categoria(4, "Educação");
                Categoria cat05 = new Categoria(5, "Imposto");
                Categoria cat06 = new Categoria(6, "Alimentação");
                Categoria cat07 = new Categoria(7, "Cartão de Credito");
                Categoria cat08 = new Categoria(8, "Cuidados Pessoais");
                Categoria cat09 = new Categoria(9, "Manutenção/Prevensão");
                Categoria cat10 = new Categoria(10, "Lazer");
                Categoria cat11 = new Categoria(11, "Vestuario");
                Categoria cat12 = new Categoria(12, "Outros");

                Tipo tipo01 = new Tipo(1, "Despesa Fixa");
                Tipo tipo02 = new Tipo(2, "Despesa Variavel");
                Tipo tipo03 = new Tipo(3, "Despesa Extra");

                _controleFinanceiroContext.Meses.AddRange(mes01, mes02, mes03, mes04, mes05, mes06, mes07, mes08, mes09, mes10, mes11, mes12);

                _controleFinanceiroContext.Categorias.AddRange(cat01, cat02, cat03, cat04, cat05, cat06, cat07, cat08, cat09, cat10, cat11, cat12);

                _controleFinanceiroContext.Tipos.AddRange(tipo01, tipo02, tipo03);

                _controleFinanceiroContext.SaveChanges();
            }
               
        }
    }
}
