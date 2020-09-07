using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MP.MKKing.Core.Models;
using MP.MKKing.Infra.Data.Context;

namespace MP.MKKing.Infra.Data.Seed
{
    /// <summary>
    /// Seed data to the database <see cref="SeedAsync"/>
    /// </summary>
    public class MKKingContextSeed
    {
        public static async Task SeedAsync(MKKingContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../MP.MKKing.Infra/Data/Seed/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var brand in brands)
                    {
                        context.ProductBrands.Add(brand);
                        // context.Entry<ProductBrand>(brand).State = EntityState.Detached;
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../MP.MKKing.Infra/Data/Seed/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var type in types)
                    {
                        context.ProductTypes.Add(type);
                        // context.Entry<ProductType>(type).State = EntityState.Detached;
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../MP.MKKing.Infra/Data/Seed/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                        // context.Entry<Product>(product).State = EntityState.Detached;
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<MKKingContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}