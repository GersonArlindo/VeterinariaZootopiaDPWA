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

namespace WebApp.Areas.Contactos.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MyRepository<Contacto> _repository;
        private INotyfService _notyfService { get; }
        private readonly IFileUploadService _fileUploadService;

        public CreateModel(MyRepository<Contacto> repository, INotyfService notyfService, IFileUploadService fileUploadService)
        {
            _repository = repository;
            _notyfService = notyfService;
            _fileUploadService = fileUploadService;
        }

        [BindProperty]
        public Contacto Contacto { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(IFormFile fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Alumno.Fotografia = await _fileUploadService.SaveFileOnAWSS3(fileUpload, Contacto.Nombre, "mycleanarchitecturebucket");
                    Contacto.Imagen = await _fileUploadService.SaveFileOnDisk(fileUpload, Contacto.NombreImagen(), "Contactos");
                    await _repository.AddAsync(Contacto);
                    _notyfService.Success("Contacto agregado exitosamente");
                }
                else
                {
                    _notyfService.Warning("Su formulario no cumple las reglas de negocio");
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
