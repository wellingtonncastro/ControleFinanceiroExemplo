using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models.ViewModels.Expense
{
    public class CreateExpenseFixedFormViewModel
    {
        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} characters long .")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
    }
}
