using System;
using System.Collections.Generic;
using System.Text;
using ControleFinanceiro.Maps;
using ControleFinanceiro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Data
{
    public class ControleFinanceiroContext : IdentityDbContext<IdentityUser, NivelAcesso,string>
    {
        public ControleFinanceiroContext(DbContextOptions<ControleFinanceiroContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Mes> Meses{ get; set; }
        public DbSet<Tipo> Tipos{ get; set; }
        public DbSet<Receita> Receitas{ get; set; }
        public DbSet<DespesaFixa> DespesaFixas{ get; set; }
        public DbSet<Despesa> Despesas{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new NivelAcessoMap());
        }
    }
}
