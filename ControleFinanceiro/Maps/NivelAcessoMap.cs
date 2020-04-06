using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Maps
{
    public class NivelAcessoMap : IEntityTypeConfiguration<NivelAcesso>
    {
        public void Configure(EntityTypeBuilder<NivelAcesso> builder)
        {
            builder.Property(nivel => nivel.descricao).IsRequired().HasMaxLength(400);

            builder.ToTable("NiveisAcessos");
        }
    }
}
