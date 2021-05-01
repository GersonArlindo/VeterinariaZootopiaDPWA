using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
   public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Foto { get; set; }
        public string NombreImagen()
        {
            return Nombre + '-' + Guid.NewGuid().ToString();
        }
        public List<Mascota> Mascotas { get; set; }
    }
}
