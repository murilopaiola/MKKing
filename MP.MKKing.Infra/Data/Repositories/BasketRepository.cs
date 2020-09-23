using System;
using System.Text.Json;
using System.Threading.Tasks;
using MP.MKKing.Core.Interfaces;
using MP.MKKing.Core.Models;
using StackExchange.Redis;

namespace MP.MKKing.Infra.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        /// <summary>
        /// Method to update an existing basket or create a new one
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        public async Task<CustomerBasket> UpdateOrCreateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(14));

            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}