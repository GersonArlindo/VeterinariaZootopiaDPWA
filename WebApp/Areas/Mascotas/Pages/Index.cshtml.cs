using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Specification;
using ApplicationCore.Specification.Filters;
using AspNetCoreHero.ToastNotification.Abstractions;
using Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Mascotas.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyRepository<Mascota> _repository;
        private readonly MyRepository<Usuario> _usuarioRepository;
        private INotyfService _notyfService { get; }

        public IndexModel(MyRepository<Mascota> repository, INotyfService notyfService, MyRepository<Usuario> usuarioRepository)
        {
            _repository = repository;
            _notyfService = notyfService;
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Usuario { get; set; }
        public List<Mascota> Mascotas { get; set; }
        public UIPaginationModel UIPagination { get; set; }

        public async Task<IActionResult> OnGetAsync(int? usuarioId, string searchString, int? currentPage, int? sizePage)
        {
            try
            {
                if (!usuarioId.HasValue)
                {
                    _notyfService.Warning($"debe seleccionar un usuario para ver sus mascotas");
                    return RedirectToPage("Index", new { area = "MascotasUsuario" });
                }
                Usuario = await _usuarioRepository.GetByIdAsync(usuarioId.Value);
                if (Usuario == null)
                {
                    _notyfService.Warning($"No se ha encontrado un usuario con el id {usuarioId.Value}");
                    return RedirectToPage("Index", new { area = "MascotasUsuario" });
                }

                var totalItems = await _repository.CountAsync(new MascotaSpec(new MascotaFilter { UsuarioId = usuarioId.Value, Nombre = searchString, LoadChildren = false, IsPagingEnabled = true }));
                UIPagination = new UIPaginationModel(currentPage, searchString, sizePage, totalItems);

                Mascotas = await _repository.ListAsync(new MascotaSpec(
                    new MascotaFilter
                    {
                        IsPagingEnabled = true,
                        Nombre = UIPagination.SearchString,
                        UsuarioId = usuarioId.Value,
                        SizePage = UIPagination.GetSizePage,
                        Page = UIPagination.GetCurrentPage
                    })
                 );
                return Page();
            }
            catch (Exception ex)
            {
                _notyfService.Error("Ocurrio un error en el servidor, intente nuevamente");
                return RedirectToPage("Index");
            }
        }
    }
}

