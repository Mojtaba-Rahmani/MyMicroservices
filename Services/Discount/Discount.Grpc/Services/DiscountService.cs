using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        #region Constructor
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository discountRepository , ILogger<DiscountService> logger)
        {
            _discountRepository = discountRepository;
            _logger = logger;
        }
        #endregion


        #region GetDiscount

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,$"Discount with ProductName {request.ProductName} is not found"));
            }

            return new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Amount = coupon.Amount,
                Description = coupon.Description
            };
        }

        #endregion
    }
}
