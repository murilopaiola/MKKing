using System;
using System.Linq.Expressions;
using MP.MKKing.Core.Models;

namespace MP.MKKing.Core.Specifications
{
    /// <summary>
    /// A specification to eager load <see cref="ProductBrand"/> and <see cref="ProductType"/> into <see cref="Product"/> DTO
    /// </summary>
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        /// <summary>
        /// Constructor that accepts sort and filter values (Criteria)
        /// </summary>
        public ProductsWithTypesAndBrandsSpecification(string sort, int? brandId, int? typeId) 
            : base(x => (!brandId.HasValue || x.ProductBrandId == brandId) && 
                        (!typeId.HasValue || x.ProductTypeId == typeId))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc": 
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                };
            }
        }

        /// <summary>
        /// Constructor with criteria
        /// </summary>
        /// <param name="id"></param>
        public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}