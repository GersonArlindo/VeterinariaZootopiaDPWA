using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Contacto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string SobreMi { get; set; }
        public string Telefono { get; set; }
        public string Imagen { get; set; }
        public string NombreImagen()
        {
            return Nombre + '-' + Guid.NewGuid().ToString();
        }
    }
}
