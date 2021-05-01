using FluentValidation;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.Validator
{
    public class MascotaValidator : AbstractValidator<Mascota>
    {
        private readonly IRepository<Usuario> _usuarioRepository;
        public MascotaValidator(IRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.Id).NotNull();

            RuleFor(x => x.UsuarioId).NotNull().WithMessage("Usuario es requerido");

            RuleFor(x => x.UsuarioId).MustAsync(async (id, cancellation) =>
            {
                return await _usuarioRepository.GetByIdAsync(id) == null ? false : true;
            }).WithMessage("debe ingresar un usuario Valido");

            RuleFor(x => x.Nombre).NotNull().WithMessage("Nombre es requerido");

            RuleFor(x => x.FechaNacimiento).NotNull().WithMessage("Fecha de nacimiento es requerido");

            RuleFor(x => x.Genero).NotNull().WithMessage("Ingrese un Genero valido");

        }
    }
}
