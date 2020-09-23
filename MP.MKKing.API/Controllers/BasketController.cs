using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MP.MKKing.Core.Interfaces;
using MP.MKKing.Core.Models;

namespace MP.MKKing.API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;

        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            // Create new empty basket if none
            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updatedBasket = await _basketRepository.UpdateOrCreateBasketAsync(basket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string basketId)
        {
            await _basketRepository.DeleteBasketAsync(basketId);
        }
    }
}