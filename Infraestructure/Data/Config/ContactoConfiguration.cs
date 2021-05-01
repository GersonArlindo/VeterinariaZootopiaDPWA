using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Data.Config
{
    public class ContactoConfiguration : IEntityTypeConfiguration<Contacto>
    {
        public void Configure(EntityTypeBuilder<Contacto> builder)
        {
            builder.ToTable("Contactos");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Codigo)
                   .HasMaxLength(16)
                   .IsRequired();

            builder.Property(ci => ci.Nombre)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(ci => ci.Telefono)
                    .IsRequired();

            builder.Property(ci => ci.Imagen)
                    .IsRequired();
        }
    }
}
