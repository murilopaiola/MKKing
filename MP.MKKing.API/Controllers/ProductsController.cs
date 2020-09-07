using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MP.MKKing.Core.Interfaces;
using MP.MKKing.Core.Models;
using MP.MKKing.Core.Specifications;

namespace MP.MKKing.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<ProductBrand> _productBrandRepository;
        private readonly IBaseRepository<ProductType> _productTypeRepository;

        public ProductsController(IBaseRepository<Product> productRepository,
            IBaseRepository<ProductBrand> productBrandRepository,
            IBaseRepository<ProductType> productTypeRepository)
        {
            _productBrandRepository = productBrandRepository;
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var specification = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productRepository.ListAsync(specification);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productRepository.GetEntityWithSpec(specification);

            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.ListAllAsync());
        }
    }
}