using ApplicationCore.Entities;
using ApplicationCore.Specification.Filters;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specification
{
    public class PlanSpec : Specification<Plan>
    {
        public PlanSpec(PlanFilter filter)
        {
            Query.OrderBy(x => x.Codigo).ThenByDescending(x => x.Id);

            if (filter.IsPagingEnabled)
                Query.Skip(PaginationHelper.CalculateSkip(filter))
                     .Take(PaginationHelper.CalculateTake(filter));

            if (!string.IsNullOrEmpty(filter.Codigo))
                Query.Search(x => x.Codigo, "%" + filter.Codigo + "%");
        }
    }
}
