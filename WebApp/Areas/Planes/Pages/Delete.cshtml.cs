using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using AspNetCoreHero.ToastNotification.Abstractions;
using Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Planes.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly MyRepository<Plan> _repository;
        private INotyfService _notyfService { get; }

        public DeleteModel(MyRepository<Plan> repository, INotyfService notyfService)
        {
            _repository = repository;
            _notyfService = notyfService;
        }

        [BindProperty]
        public Plan Plan { get; set; }

        public async Task<IActionResult> OnGetAsync(int Id)
        {
            Plan = await _repository.GetByIdAsync(Id);
            if (Plan == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int Id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var PlanToDelete = await _repository.GetByIdAsync(Id);
            if (PlanToDelete == null)

                return NotFound();

            PlanToDelete.Codigo = Plan.Codigo;
            PlanToDelete.Nombre = Plan.Nombre;
            PlanToDelete.Descripcion = Plan.Descripcion;
            PlanToDelete.Precio = Plan.Precio;

            await _repository.DeleteAsync(PlanToDelete);
            return RedirectToPage("./Index");
        }

    }
}
