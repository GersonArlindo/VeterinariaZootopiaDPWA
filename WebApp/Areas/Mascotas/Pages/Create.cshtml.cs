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

namespace WebApp.Areas.Mascotas.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MyRepository<Mascota> _repository;
        private readonly MyRepository<Usuario> _usuarioRepository;
        private INotyfService _notyfService { get; }
        private readonly IFileUploadService _fileUploadService;

        public CreateModel(MyRepository<Mascota> repository, INotyfService notyfService, MyRepository<Usuario> usuarioRepository, IFileUploadService fileUploadService)
        {
            _repository = repository;
            _notyfService = notyfService;
            _fileUploadService = fileUploadService;
            _usuarioRepository = usuarioRepository;
        }

        [BindProperty]
        public Mascota Mascota  { get; set; }

        public Usuario Usuario { get; set; }

        public async Task<IActionResult> OnGet(int UsuarioId)
        {
            try
            {
                Usuario = await _usuarioRepository.GetByIdAsync(UsuarioId);
                if (Usuario == null)
                {
                    _notyfService.Warning($"No se ha encontrado un usuario con ese id {UsuarioId}");
                    return RedirectToPage("Index", new { area = "MascotasUsuario" });
                }

                Mascota = new Mascota
                {
                    UsuarioId = UsuarioId
                };
                return Page();
            }
            catch (Exception ex)
            {
                _notyfService.Error("Ocurrio un error en el servidor, intente nuevamente");
                return RedirectToPage("Index");
            }
        }

        public async Task<IActionResult> OnPost(IFormFile fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Alumno.Fotografia = await _fileUploadService.SaveFileOnAWSS3(fileUpload, Producto.Nombre, "mycleanarchitecturebucket");
                    Mascota.Foto = await _fileUploadService.SaveFileOnDisk(fileUpload, Mascota.NombreImagen(), "mascotas");
                    await _repository.AddAsync(Mascota);
                    _notyfService.Success("Mascota agregada exitosamente");
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
