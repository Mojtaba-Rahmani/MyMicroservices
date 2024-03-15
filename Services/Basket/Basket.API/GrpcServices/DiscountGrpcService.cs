using Discount.Grpc.Protos;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        #region Constructor

        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }
        #endregion


        #region get Discount

        public async Task<CouponModel> GetDiscount(string ProductName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = ProductName };

            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }

        #endregion
    }
}
