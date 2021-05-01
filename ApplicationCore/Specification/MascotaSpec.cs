using ApplicationCore.Entities;
using ApplicationCore.Specification.Filters;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specification
{
    public class MascotaSpec : Specification<Mascota>
    {
        public MascotaSpec(MascotaFilter filter)
        {
            Query.OrderBy(x => x.Nombre).ThenByDescending(x => x.Id);

            if (filter.IsPagingEnabled)
                Query.Skip(PaginationHelper.CalculateSkip(filter))
                     .Take(PaginationHelper.CalculateTake(filter));

            if (!string.IsNullOrEmpty(filter.Nombre))
                Query.Search(x => x.Nombre, "%" + filter.Nombre + "%");

            Query.Where(x => x.UsuarioId == filter.UsuarioId);
        }
    }
}
