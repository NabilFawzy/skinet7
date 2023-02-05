using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity 
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> specs){
          
            var query=inputQuery;

            if(specs.WhereCriteria!=null){
                query=query.Where(specs.WhereCriteria);
            }
            query=specs.Includes.Aggregate(query,(current,include)=>current.Include(include));

            return query;
        }
    }
}