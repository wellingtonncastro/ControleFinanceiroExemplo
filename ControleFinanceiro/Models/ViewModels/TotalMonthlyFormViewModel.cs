using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models.ViewModels
{
    public class TotalMonthlyFormViewModel
    {
        [Display(Name = "Total da receita: ")]
        public decimal TotalReceita { get; set; }

        [Display(Name = "Total da minha despesas fixas: ")]
        public decimal TotalDespesaFixa { get; set; }

        [Display(Name = "Total da minha despesa variável: ")]
        public decimal TotalDespesaVariavel { get; set; }

        [Display(Name = "Total da minha despesa extra: ")]
        public decimal TotalDespesaExtra { get; set; }

        [Display(Name = "Meu saldo: ")]
        public decimal Saldo { get; set; }

        [Display(Name = "Valor para todos os membros da familia com base no saldo: ")]
        public decimal MembroDaFamilia { get; set; }

        [Display(Name = "Valor recomendado para guardar com base no saldo : ")]
        public decimal ValorGuardar { get; set; }

        [Display(Name = "Total das despesas : ")]
        public decimal TotalDespesa { get; set; }
    }
}
