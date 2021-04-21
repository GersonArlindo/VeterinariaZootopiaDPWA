using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Enum
{
    public enum Tipo
    {
        [Display(Name = "Comida")]
        Comida,
        [Display(Name = "Accesorios")]
        Accesorios,
        [Display(Name = "Juguetes")]
        Juguetes,
        [Display(Name = "Baño")]
        Baño,
        [Display(Name = "Otros")]
        Otros
    }
}
