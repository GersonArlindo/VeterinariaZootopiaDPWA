using ApplicationCore.Entities;
using ApplicationCore.Specification.Filters;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specification
{
    public class UsuarioSpec : Specification<Usuario>
    {
        public UsuarioSpec(UsuarioFilter filter)
        {
            Query.OrderBy(x => x.Nombre).ThenByDescending(x => x.Id);

            if (filter.IsPagingEnabled)
                Query.Skip(PaginationHelper.CalculateSkip(filter))
                     .Take(PaginationHelper.CalculateTake(filter));

            if (filter.LoadChildren)
                Query.Include(x => x.Mascotas);

            if (!string.IsNullOrEmpty(filter.Nombre))
                Query.Search(x => x.Nombre, "%" + filter.Nombre + "%");
        }
    }
}
