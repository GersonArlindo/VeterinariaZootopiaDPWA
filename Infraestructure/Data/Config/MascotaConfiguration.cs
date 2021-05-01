using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
    {
        public void Configure(EntityTypeBuilder<Mascota> builder)
        {
            builder.ToTable("Mascotas");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.UsuarioId);

            builder.Property(ci => ci.Nombre)
                   .HasMaxLength(16)
                   .IsRequired();

            builder.Property(ci => ci.FechaNacimiento)
                    .IsRequired();

            builder.Property(ci => ci.Genero)
                    .IsRequired();

            builder.Property(ci => ci.Foto)
                    .IsRequired();
        }
    }
}
