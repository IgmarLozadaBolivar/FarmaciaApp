using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
    {
        public void Configure(EntityTypeBuilder<Ciudad> builder)
        {
            builder.ToTable("Ciudad");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired().HasMaxLength(3);
            builder.Property(c => c.Nombre).IsRequired().HasColumnName("Nombre").HasColumnType("varchar(50)").HasComment("Nombre de la ciudad").HasMaxLength(50);

            builder.HasOne(p => p.Departamento).WithMany(p => p.Ciudades).HasForeignKey(p => p.IdDepFK);
        }
    }
}