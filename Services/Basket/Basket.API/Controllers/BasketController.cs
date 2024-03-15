using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        #region Constructor
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountService;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountService)
        {
            _basketRepository = basketRepository;
            _discountService = discountService;
        }
        #endregion

        #region GetBasket
        [HttpGet("{userName}" , Name ="GetBasket")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetUserBasket(userName);
                return Ok(basket ?? new ShoppingCart(userName));
        }
        #endregion

        #region UpdateBasket
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            // ToDo : Get data From discount.grpc and calculate finall price of Product

            foreach (var item in basket.Items)
            {
               var coupon = await _discountService.GetDiscount(item.ProductName);
               item.Price -= coupon.Amount; 
            }

            return Ok(await _basketRepository.UpdateBasket(basket));
        }
        #endregion

        #region RemoveBasket
        [HttpDelete("{userName}" , Name = "DeleteBasket")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
        #endregion
    }
}
