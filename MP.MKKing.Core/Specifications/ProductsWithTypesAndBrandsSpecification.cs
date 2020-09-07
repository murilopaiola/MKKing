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
        /// Constructor to set Includes only
        /// </summary>
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

        /// <summary>
        /// Constructor to set Criteria and Includes
        /// </summary>
        /// <param name="id"></param>
        public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}