using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MP.MKKing.API.Controllers;
using MP.MKKing.API.DTOs;
using MP.MKKing.API.Errors;
using MP.MKKing.API.Helpers;
using MP.MKKing.Core.Interfaces;
using MP.MKKing.Core.Models;
using MP.MKKing.Core.Specifications;

namespace MP.MKKing.API
{
    public class ProductsController : BaseApiController
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<ProductBrand> _productBrandRepository;
        private readonly IBaseRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IBaseRepository<Product> productRepository,
            IBaseRepository<ProductBrand> productBrandRepository,
            IBaseRepository<ProductType> productTypeRepository, 
            IMapper mapper)
        {
            _productBrandRepository = productBrandRepository;
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// A method to get a list of products
        /// </summary>
        /// <param name="productParams"></param>
        /// <returns>Paginated products result</returns>
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecificationParameters productParams)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var products = await _productRepository.ListAsync(specification);

            var totalItems = await _productRepository.CountAsync(countSpec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        /// <summary>
        /// A method to get a product by id
        /// </summary>
        /// <param name="id">id of the product</param>
        /// <returns>A product</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productRepository.GetEntityWithSpec(specification);

            if (null == product) return NoContent();

            return Ok(_mapper.Map<Product, ProductDto>(product));
        }

        /// <summary>
        /// A method to get all product brands
        /// </summary>
        /// <returns>all product brands</returns>
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.ListAllAsync());
        }
        
        /// <summary>
        /// A method to get all product types
        /// </summary>
        /// <returns>all product types</returns>
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.ListAllAsync());
        }
    }
}