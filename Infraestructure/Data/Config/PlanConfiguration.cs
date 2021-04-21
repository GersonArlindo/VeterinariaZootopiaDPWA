using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Data.Config
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("Planes");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Codigo)
                   .HasMaxLength(12)
                   .IsRequired();

            builder.Property(ci => ci.Nombre)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(ci => ci.Descripcion)
                    .IsRequired();

            builder.Property(ci => ci.Precio)
                    .IsRequired();
        }
    }
}
