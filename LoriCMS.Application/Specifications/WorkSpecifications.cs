using LoriCMS.Domain.AggregatesModel;
using LoriCMS.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoriCMS.Application.Specifications
{
    public static class WorkSpecifications
    {
        public static ISpecification<Work_Item> ByTitle(string key)
        {
            Specification<Work_Item> spec = new TrueSpecification<Work_Item>();

            if (!string.IsNullOrWhiteSpace(key))
            {
                spec &= new DirectSpecification<Work_Item>(o => o.Content.Title == key);
            }
            return spec;
        }

        public static ISpecification<Work_Item> ByDate(DateTime? from, DateTime? to)
        {
            Specification<Work_Item> spec = new TrueSpecification<Work_Item>();

            if (from.HasValue)
            {
                spec &= new DirectSpecification<Work_Item>(o => o.DataCreateDateTime >= from.Value);
            }

            if (to.HasValue)
            {
                spec &= new DirectSpecification<Work_Item>(o => o.DataCreateDateTime < to.Value);
            }
            return spec;
        }
    }
}
