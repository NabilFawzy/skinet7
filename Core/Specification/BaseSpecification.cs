using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
         public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> whereCriteria)
        {
            WhereCriteria = whereCriteria;
        }

        public Expression<Func<T, bool>> WhereCriteria {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy  {get; private set;}

        public Expression<Func<T, object>> OrderByDescending   {get; private set;}

        protected void AddInclude( Expression<Func<T, object>> includeExpression){
            Includes.Add(includeExpression);
        }

         protected void AddOrderBy( Expression<Func<T, object>> OrderByExpression){
            OrderBy = OrderByExpression;
        }
         protected void AddOrderByDescending( Expression<Func<T, object>> OrderByDescendingExpression){
            OrderByDescending = OrderByDescendingExpression;
        }



        
        public int Take {get; private set;}

        public int Skip {get; private set;}

        public bool IsPagingEnabled {get; private set;}


        protected void ApplyPaging(int skip,int take){
            Take=take;
            Skip=skip;
            
            IsPagingEnabled=true;
        }

    }
}