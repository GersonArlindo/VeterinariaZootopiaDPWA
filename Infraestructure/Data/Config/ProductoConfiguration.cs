using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Codigo)
                   .HasMaxLength(16)
                   .IsRequired();

            builder.Property(ci => ci.Nombre)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(ci => ci.Descripcion)
                    .IsRequired();

            builder.Property(ci => ci.Tipo)
                    .IsRequired();

            builder.Property(ci => ci.Precio)
                    .IsRequired();

            builder.Property(ci => ci.Imagen)
                    .IsRequired();
        }
    }
}
