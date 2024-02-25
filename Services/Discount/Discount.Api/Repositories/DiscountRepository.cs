using Discount.Api.Entities;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        #region Constructor
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion


        #region Get Coupon
        public async Task<Coupon> GetDiscount(string ProductName)
        {
            using var connection = new NpgsqlConnection(
                                _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName"
                , new { ProductName = ProductName });

            if (coupon == null)
            {
                return new Coupon
                {
                    ProductName = "No Discount"
                    ,
                    Amount = 0
                    ,
                    Description = "No Discount Description"
                };
            }
        

            return coupon;

        }
        #endregion

        #region Create Coupon
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(
                      _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                (
                "INSERT INTO Coupon (ProductName,Description,Amount) " +
                "VALUES(@ProductName,@Description,@Amount)"
                , new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount
                }
                );
            if (affected == 0)
                return false;
            else
                return true;
        }
        #endregion

        #region Update Coupon
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(
              _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                (
                "UPDATE Coupon SET ProductName=@ProductName, Description=@Description,Amount=@Amount where id=@CouponId"
                , new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                    CouponId = coupon.Id
                }
                );

            if (affected == 0)
                return false;
            else
                return true;

        }
        #endregion

        #region Delete Coupon
        public async Task<bool> DeleteDiscount(string ProductName)
        {
            using var connection = new NpgsqlConnection(
         _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                (
                "DELETE FROM Coupon WHERE ProductName=@ProductName"
                , new{ ProductName = ProductName }
                );

            if (affected == 0)
                return false;
            else
                return true;
        }
        #endregion



    }
}
