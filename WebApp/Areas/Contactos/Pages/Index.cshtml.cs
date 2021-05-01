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

namespace WebApp.Areas.Contactos.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyRepository<Contacto> _repository;
        private INotyfService _notyfService { get; }

        public IndexModel(MyRepository<Contacto> repository, INotyfService notyfService)
        {
            _repository = repository;
            _notyfService = notyfService;
        }

        public List<Contacto> Contactos { get; set; }
        public UIPaginationModel UIPagination { get; set; }

        public async Task OnGetAsync(string searchString, int? currentPage, int? sizePage)
        {
            var totalItems = await _repository.CountAsync(new ContactoSpec(new ContactoFilter { Nombre = searchString, LoadChildren = false, IsPagingEnabled = true }));
            UIPagination = new UIPaginationModel(currentPage, searchString, sizePage, totalItems);

            Contactos = await _repository.ListAsync(new ContactoSpec(
                new ContactoFilter
                {
                    IsPagingEnabled = true,
                    Nombre = UIPagination.SearchString,
                    SizePage = UIPagination.GetSizePage,
                    Page = UIPagination.GetCurrentPage
                })
             );
        }
    }
}
