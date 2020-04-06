using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Models
{
    public class Receita
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
      

    }
}
