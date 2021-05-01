using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Validator
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nombre).NotNull().WithMessage("Nombre es requerido");

            RuleFor(x => x.Apellido).NotNull().WithMessage("Apellido es requerido");

            RuleFor(x => x.Direccion).NotNull().WithMessage("Direccion es requerido");

        }
    }
}
