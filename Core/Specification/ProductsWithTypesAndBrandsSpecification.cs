using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification( ProductSpecParams productParams)
        :base(x=>
             ( String.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))&&
              (!productParams.brandId.HasValue || x.ProductBrandId==productParams.brandId)&&
              (!productParams.typeId.HasValue || x.ProductTypeId==productParams.typeId)
            )
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
            AddOrderBy(x=>x.Name);
            ApplyPaging(productParams.PageSize*(productParams.PageIndex*-1),productParams.PageSize);

            if(!String.IsNullOrEmpty(productParams.Sort)){

                  switch(productParams.Sort){
                    case "priceAsc":
                           AddOrderBy(x=>x.Price);
                           break;
                    case "priceDesc":
                           AddOrderByDescending(x=>x.Price);
                           break;
                    case "nameAsc":
                           AddOrderBy(x=>x.Name);
                           break;
                    case "nameDesc":
                           AddOrderByDescending(x=>x.Name);
                           break;
                  }       
             }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id==id)
        {
             AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
    }
}