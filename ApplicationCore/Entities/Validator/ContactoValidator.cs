using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Validator
{
    public class ContactoValidator : AbstractValidator<Contacto>
    {
        public ContactoValidator()
        {
            RuleFor(x => x.Id).NotNull();

            RuleFor(x => x.Codigo).NotNull().WithMessage("Codigo es requerido");

            RuleFor(x => x.Nombre).NotNull().WithMessage("Nombre es requerido")
           .Length(3, 50).WithMessage("El Nombre debe contener entre 3 y 50 caracteres");


            RuleFor(x => x.Telefono).NotNull().WithMessage("Telefono es requerido");

            //RuleFor(x => x.Imagen).NotNull().WithMessage("Url de imagen es requerida");
        }
    }
}
