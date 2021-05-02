using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using AspNetCoreHero.ToastNotification.Abstractions;
using Infraestructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Services;

namespace WebApp.Areas.MascotasUsuario.Pages
{
    public class EditModel : PageModel
    {
        private readonly MyRepository<Usuario> _repository;
        private INotyfService _notyfService { get; }

        public EditModel(MyRepository<Usuario> repository, INotyfService notyfService)
        {
            _repository = repository;
            _notyfService = notyfService;
        }


        [BindProperty]
        public Usuario Usuario { get; set; }
        public async Task<IActionResult> OnGet(int Id)
        {
            Usuario = await _repository.GetByIdAsync(Id);
            if (ModelState.IsValid)
            {
                return Page();

            }
            return NotFound();
        }
        public async Task<IActionResult> OnPost(int Id)
        {
            try
            {
                var UsuarioToUpdate = await _repository.GetByIdAsync(Id);
                if (UsuarioToUpdate == null)
                {
                    _notyfService.Warning("No se encontro el usuario con ese id");
                    return RedirectToPage("Index");
                }

                if (ModelState.IsValid)
                {
                    UsuarioToUpdate.Nombre = Usuario.Nombre;
                    UsuarioToUpdate.Apellido = Usuario.Apellido;
                    UsuarioToUpdate.Direccion = Usuario.Direccion;

                    await _repository.UpdateAsync(UsuarioToUpdate);
                    _notyfService.Success("Usuario editado exitosamente");
                }
                else
                {
                    _notyfService.Warning("El formulario no cumple con las reglas del negocio");
                    return Page();

                }
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {

                _notyfService.Error("Ocurrio un error en el servidor, intente nuevamente");
                return RedirectToPage("Index");

            }
        }
    }
}
