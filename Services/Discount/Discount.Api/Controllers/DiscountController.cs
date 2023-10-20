using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Discount.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : Controller
    {
        #region Constructor
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        #endregion

        #region Get Coupon

        [HttpGet("{ProductName}" , Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string ProductName)
        {
            var coupon = await _discountRepository.GetDiscount(ProductName);

            return Ok(coupon);  
        }

        #endregion

        #region Create Coupon

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            await _discountRepository.CreateDiscount(coupon);

            return CreatedAtRoute("GetDiscount", new
            {
                ProductName = coupon.ProductName
            },coupon);

        }

        #endregion

        #region 

        #region Edit Coupon

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
          return Ok(await _discountRepository.UpdateDiscount(coupon));

        }

        #endregion

        #endregion Delete Coupon

        [HttpDelete("{ProductName}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> DeleteDiscount(string ProductName)
        {
            return Ok(await _discountRepository.DeleteDiscount(ProductName));

        }

        #region 



        #endregion

        #region 



        #endregion
    }
}
