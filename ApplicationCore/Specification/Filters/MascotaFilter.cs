using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specification.Filters
{
    public class MascotaFilter : BaseFilter
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
    }
}
