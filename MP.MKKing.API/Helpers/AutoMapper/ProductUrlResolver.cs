using AutoMapper;
using Microsoft.Extensions.Configuration;
using MP.MKKing.API.DTOs;
using MP.MKKing.Core.Models;

namespace MP.MKKing.API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Resolve the <see cref="Product"/> image url using AutoMapper
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="destMember"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context) 
            => !string.IsNullOrEmpty(source.PictureUrl) ? _configuration["ApiUrl"] + source.PictureUrl : null;
    }
}