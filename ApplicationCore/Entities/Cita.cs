using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
   public class Cita
   {
        public int Id { get; set; }
        public string CodigoCita { get; set; }
        public string DescripcionCita { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal Precio { get; set; }
        
    }
}
