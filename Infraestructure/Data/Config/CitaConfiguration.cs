using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Data.Config
{
    public class Citafiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("Citas");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.CodigoCita)
                   .HasMaxLength(12)
                   .IsRequired();

            builder.Property(ci => ci.DescripcionCita)
                    .IsRequired();

            builder.Property(ci => ci.FechaHora)
                    .IsRequired();

            builder.Property(ci => ci.Precio)
                    .IsRequired();
        }
    }
}
