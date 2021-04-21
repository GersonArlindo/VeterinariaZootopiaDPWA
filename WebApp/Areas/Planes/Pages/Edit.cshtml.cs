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
    public class EditModel : PageModel
    {
        private readonly MyRepository<Plan> _repository;
        private INotyfService _notyfService { get; }

        public EditModel(MyRepository<Plan> repository, INotyfService notyfService)
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

            var PlanToUpdate = await _repository.GetByIdAsync(Id);
            if (PlanToUpdate == null)

                return NotFound();

            PlanToUpdate.Codigo = Plan.Codigo;
            PlanToUpdate.Nombre = Plan.Nombre;
            PlanToUpdate.Descripcion = Plan.Descripcion;
            PlanToUpdate.Precio = Plan.Precio;

            await _repository.UpdateAsync(PlanToUpdate);
            return RedirectToPage("./Index");
        }
    }
}
