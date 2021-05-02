using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using AspNetCoreHero.ToastNotification.Abstractions;
using Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Citas.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly MyRepository<Cita> _repository;
        private INotyfService _notyfService { get; }

        public DeleteModel(MyRepository<Cita> repository, INotyfService notyfService)
        {
            _repository = repository;
            _notyfService = notyfService;
        }

        [BindProperty]
        public Cita Cita { get; set; }

        public async Task<IActionResult> OnGetAsync(int Id)
        {
            Cita = await _repository.GetByIdAsync(Id);
            if (Cita == null)
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

            var CitaToDelete = await _repository.GetByIdAsync(Id);
            if (CitaToDelete == null)
                return NotFound();

            CitaToDelete.CodigoCita = Cita.CodigoCita;
            CitaToDelete.DescripcionCita = Cita.DescripcionCita;
            CitaToDelete.FechaHora = Cita.FechaHora;
            CitaToDelete.Precio = Cita.Precio;

            await _repository.DeleteAsync(CitaToDelete);
            _notyfService.Success("Producto eliminado exitosamente");
            return RedirectToPage("./Index");
        }

    }
}
