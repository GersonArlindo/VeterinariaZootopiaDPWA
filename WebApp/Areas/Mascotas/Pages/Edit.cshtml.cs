using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using AspNetCoreHero.ToastNotification.Abstractions;
using Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Mascotas.Pages
{
    public class EditModel : PageModel
    {
        private readonly MyRepository<Mascota> _repository;
        private readonly MyRepository<Usuario> _usuarioRepository;
        private INotyfService _notyfService { get; }

        public EditModel(MyRepository<Mascota> repository, INotyfService notyfService, MyRepository<Usuario> usuarioRepository)
        {
            _repository = repository;
            _notyfService = notyfService;
            _usuarioRepository = usuarioRepository;
        }


        [BindProperty]
        public Mascota Mascota { get; set; }
        public Usuario Usuario { get; set; }
        public async Task<IActionResult> OnGet(int Id, int UsuarioId)
        {
            try
            {
                Usuario = await _usuarioRepository.GetByIdAsync(UsuarioId);
                if (Usuario == null)
                {
                    _notyfService.Warning($"No se ha encontrado el usuaario con el id {UsuarioId}");
                    return RedirectToPage("Index", new { area = "Mascotas" });

                }
                var mascota = await _repository.GetByIdAsync(Id);
                if (mascota == null)
                {
                    _notyfService.Warning($"No se ha encontrado el registro con el id {Id}");
                    return RedirectToPage("Index", new { area = "Mascotas" });
                }
                Mascota = mascota;

                return Page();
            }
            catch (Exception ex)
            {
                _notyfService.Error("Ocurrio un error en el servidor, intente nuevamente");
                return RedirectToPage("Index");
            }


        }
        public async Task<IActionResult> OnPost(int Id)
        {
            try
            {
                var MascotaToUpdate = await _repository.GetByIdAsync(Id);
                if (MascotaToUpdate == null)
                {
                    _notyfService.Warning("No se encontro la mascota con ese id");
                    return RedirectToPage("Index");
                }

                if (ModelState.IsValid)
                {
                    MascotaToUpdate.Nombre = Mascota.Nombre;
                    MascotaToUpdate.UsuarioId = Mascota.UsuarioId;
                    MascotaToUpdate.FechaNacimiento = Mascota.FechaNacimiento;
                    MascotaToUpdate.Genero = Mascota.Genero;

                    await _repository.UpdateAsync(MascotaToUpdate);
                    _notyfService.Success("Mascota editada exitosamente");
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
