using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class GeneroConfiguration : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Genero");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id).IsRequired().HasMaxLength(3);
            builder.Property(g => g.Nombre).IsRequired().HasColumnName("Nombre").HasColumnType("varchar(50)").HasComment("Nombre del genero").HasMaxLength(50);
            builder.Property(g => g.Abreviatura).IsRequired().HasColumnName("Abreviatura").HasColumnType("varchar(3)").HasComment("Abreviatura del genero").HasMaxLength(3);
            builder.Property(g => g.Descripcion).IsRequired().HasColumnName("Descripcion").HasColumnType("varchar(30)").HasComment("Descripcion del genero").HasMaxLength(30);
        }
    }
}