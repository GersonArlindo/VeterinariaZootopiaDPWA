using ApplicationCore.Enum;

namespace ApplicationCore.Specification.Filters
{
    public class ProductoFilter : BaseFilter
    {
        public Tipo Tipo { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
}
