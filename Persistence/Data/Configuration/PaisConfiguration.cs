using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class PaisConfiguration : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Pais");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).IsRequired().HasMaxLength(3);
            builder.Property(p => p.Nombre).IsRequired().HasColumnName("Nombre").HasColumnType("varchar(50)").HasComment("Nombre del pais").HasMaxLength(50);
            builder.Property(p => p.Capital).IsRequired().HasColumnName("Capital").HasColumnType("varchar(30)").HasComment("Capital del pais").HasMaxLength(30);
            builder.Property(p => p.CodISO).IsRequired().HasColumnName("Codigo ISO").HasColumnType("varchar(3)").HasComment("Abreviatura del pais").HasMaxLength(3);
            builder.Property(p => p.Moneda).IsRequired().HasColumnName("Moneda").HasColumnType("varchar(30)").HasComment("Moneda del pais").HasMaxLength(30);
            builder.Property(p => p.Idioma).IsRequired().HasColumnName("Idioma").HasColumnType("varchar(20)").HasComment("Idioma del pais").HasMaxLength(20);
        }
    }
}