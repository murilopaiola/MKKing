using MP.MKKing.Core.Models;

namespace MP.MKKing.Core.Specifications
{
    /// <summary>
    /// Just to get the count of items to populate <see cref="Pagination" class/>
    /// </summary>
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecificationParameters productParams)
            : base(x => (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
                        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {
            
        }
        
    }
}