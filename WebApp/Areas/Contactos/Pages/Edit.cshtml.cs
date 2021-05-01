using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using AspNetCoreHero.ToastNotification.Abstractions;
using Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Contactos.Pages
{
    public class EditModel : PageModel
    {
        private readonly MyRepository<Contacto> _repository;
        private INotyfService _notyfService { get; }

        public EditModel(MyRepository<Contacto> repository, INotyfService notyfService)
        {
            _repository = repository;
            _notyfService = notyfService;
        }


        [BindProperty]
        public Contacto Contacto { get; set; }
        public async Task<IActionResult> OnGet(int Id)
        {
            Contacto = await _repository.GetByIdAsync(Id);
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
                var ContactoToUpdate = await _repository.GetByIdAsync(Id);
                if (ContactoToUpdate == null)
                {
                    _notyfService.Warning("No se encontro el Contacto con ese id");
                    return RedirectToPage("Index");
                }

                if (ModelState.IsValid)
                {
                    ContactoToUpdate.Codigo = Contacto.Codigo;
                    ContactoToUpdate.Nombre = Contacto.Nombre;
                    ContactoToUpdate.SobreMi = Contacto.SobreMi;
                    ContactoToUpdate.Telefono = Contacto.Telefono;
                    ContactoToUpdate.Imagen = Contacto.Imagen;

                    await _repository.UpdateAsync(ContactoToUpdate);
                    _notyfService.Success("Contacto editado exitosamente");
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
