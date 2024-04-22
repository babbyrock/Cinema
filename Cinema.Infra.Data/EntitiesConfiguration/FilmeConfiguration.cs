using Cinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infra.Data.EntitiesConfiguration
{
    public class FilmeConfiguration : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Nome).HasMaxLength(100).IsRequired();
            builder.Property(t => t.Duracao).HasMaxLength(8).IsRequired();
            builder.HasOne(s => s.Sala).WithMany(s => s.Filmes);
        }
    }
}
