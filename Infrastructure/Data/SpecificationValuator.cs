using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationValuator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> spec){

                var query = inputQuery;

                if (spec.Criteria != null)
                {
                    query = query.Where(spec.Criteria); //p => p.ProductTypeId == id
                }

                query = spec.Includes.Aggregate(query,(current,include) => 
                    current.Include(include));
                return query;

        }
    }
}