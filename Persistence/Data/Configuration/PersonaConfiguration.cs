using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).IsRequired().HasMaxLength(3);
            builder.Property(p => p.Nombres).IsRequired().HasColumnName("Nombres").HasColumnType("varchar(50)").HasComment("Nombres de la persona").HasMaxLength(50);
            builder.Property(p => p.Apellidos).IsRequired().HasColumnName("Apellidos").HasColumnType("varchar(50)").HasComment("Apellidos de la persona").HasMaxLength(50);
            builder.Property(p => p.Edad).IsRequired().HasColumnName("Edad").HasColumnType("int").HasComment("Edad de la persona").HasMaxLength(3);
            builder.Property(p => p.FechaNac).IsRequired().HasColumnName("Fecha de Nacimiento").HasColumnType("date").HasComment("Fecha de nacimiento de la persona");

            builder.HasOne(p => p.Ciudad).WithMany(p => p.Personas).HasForeignKey(p => p.IdCiuFK);
            builder.HasOne(p => p.Genero).WithMany(p => p.Personas).HasForeignKey(p => p.IdGenFK);
            builder.HasOne(p => p.TipoPersona).WithMany(p => p.Personas).HasForeignKey(p => p.IdTipoPerFK);
        }
    }
}