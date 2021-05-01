using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Nombre)
                   .HasMaxLength(16)
                   .IsRequired();

            builder.Property(ci => ci.Apellido)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(ci => ci.Direccion)
                    .IsRequired();

            builder.Property(ci => ci.Foto)
                    .IsRequired();
        }

    }
}
