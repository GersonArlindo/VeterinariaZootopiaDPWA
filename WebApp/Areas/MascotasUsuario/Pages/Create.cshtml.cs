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
    public class CreateModel : PageModel
    {
        private readonly MyRepository<Usuario> _repository;
        private INotyfService _notyfService { get; }
        private readonly IFileUploadService _fileUploadService;

        public CreateModel(MyRepository<Usuario> repository, INotyfService notyfService, IFileUploadService fileUploadService)
        {
            _repository = repository;
            _notyfService = notyfService;
            _fileUploadService = fileUploadService;
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(IFormFile fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Alumno.Fotografia = await _fileUploadService.SaveFileOnAWSS3(fileUpload, Producto.Nombre, "mycleanarchitecturebucket");
                    Usuario.Foto = await _fileUploadService.SaveFileOnDisk(fileUpload, Usuario.NombreImagen(), "usuarios");
                    await _repository.AddAsync(Usuario);
                    _notyfService.Success("Usuario agregado exitosamente");
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

                //_notyfService.Error("Ocurrio un error en el servidor, intente nuevamente");
                throw new Exception("Mensaje.", ex);
            }
        }
    }
}
