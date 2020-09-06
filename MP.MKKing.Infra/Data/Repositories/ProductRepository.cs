using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MP.MKKing.Core.Interfaces;
using MP.MKKing.Core.Models;
using MP.MKKing.Infra.Data.Context;

namespace MP.MKKing.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MKKingContext _context;

        public ProductRepository(MKKingContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}