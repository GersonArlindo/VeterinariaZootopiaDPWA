using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Validator
{
    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            RuleFor(x => x.Id).NotNull();

            RuleFor(x => x.Codigo).NotNull().WithMessage("Codigo es requerido");

            RuleFor(x => x.Nombre).NotNull().WithMessage("Nombre es requerido")
           .Length(3, 50).WithMessage("El Nombre debe contener entre 3 y 50 caracteres");

            RuleFor(x => x.Descripcion).NotNull().WithMessage("Descripcion es requerido");

            RuleFor(x => x.Tipo).IsInEnum().WithMessage("Ingrese un Tipo valido");

            RuleFor(x => x.Precio).NotNull().WithMessage("Precio es requerido");
        }
    }
}
