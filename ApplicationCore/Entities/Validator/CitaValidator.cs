using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Validator
{
    public class CitaValidator : AbstractValidator<Cita>
    {
        public CitaValidator()
        {
            RuleFor(x => x.Id).NotNull();

            RuleFor(x => x.CodigoCita).NotNull().WithMessage("Codigo es requerido");

            RuleFor(x => x.DescripcionCita).NotNull().WithMessage("La descripcion es requerida");

            RuleFor(x => x.FechaHora).NotNull().WithMessage("La fecha y hora es requerida");

            RuleFor(x => x.Precio).NotNull().WithMessage("Precio es requerido");
        }
    }
}
