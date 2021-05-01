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

namespace WebApp.Areas.MascotasUsuario.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyRepository<Usuario> _repository;
        private INotyfService _notyfService { get; }

        public IndexModel(MyRepository<Usuario> repository, INotyfService notyfService)
        {
            _repository = repository;
            _notyfService = notyfService;
        }

        public List<Usuario> Usuarios { get; set; }
        public UIPaginationModel UIPagination { get; set; }

        public async Task OnGetAsync(string searchString, int? currentPage, int? sizePage)
        {
            var totalItems = await _repository.CountAsync(new UsuarioSpec(new UsuarioFilter { Nombre = searchString, LoadChildren = false, IsPagingEnabled = true }));
            UIPagination = new UIPaginationModel(currentPage, searchString, sizePage, totalItems);

            Usuarios = await _repository.ListAsync(new UsuarioSpec(
                new UsuarioFilter
                {
                    IsPagingEnabled = true,
                    Nombre = UIPagination.SearchString,
                    SizePage = UIPagination.GetSizePage,
                    Page = UIPagination.GetCurrentPage,
                    LoadChildren = true,
                })
             );
        }

    }
}
