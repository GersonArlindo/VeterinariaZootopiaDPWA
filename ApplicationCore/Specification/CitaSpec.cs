using ApplicationCore.Entities;
using ApplicationCore.Specification.Filters;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specification
{
    public class CitaSpec: Specification<Cita>
    {
        public CitaSpec(CitaFilter filter)
        {
            Query.OrderBy(x => x.CodigoCita).ThenByDescending(x => x.Id);

            if (filter.IsPagingEnabled)
                Query.Skip(PaginationHelper.CalculateSkip(filter))
                     .Take(PaginationHelper.CalculateTake(filter));

            if (!string.IsNullOrEmpty(filter.CodigoCita))
                Query.Search(x => x.CodigoCita, "%" + filter.CodigoCita + "%");
        }
    }
}
