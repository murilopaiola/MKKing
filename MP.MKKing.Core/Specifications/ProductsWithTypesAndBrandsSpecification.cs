using System;
using System.Linq.Expressions;
using MP.MKKing.Core.Models;

namespace MP.MKKing.Core.Specifications
{
    /// <summary>
    /// A specification to eager load <see cref="ProductBrand"/> and <see cref="ProductType"/> into a <see cref="Product"/> DTO.
    /// This class accepts <see cref="ProductSpecificationParameters"/>
    /// </summary>
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        /// <summary>
        /// Constructor that takes a <see cref="ProductSpecificationParameters"/>
        /// </summary>
        /// <param name="productParams"></param>
        public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParameters productParams) 
            : base(x => (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
                        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
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