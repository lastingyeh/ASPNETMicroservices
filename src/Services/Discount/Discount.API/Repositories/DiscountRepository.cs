using System.Threading.Tasks;
using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string _connectionString;
        public DiscountRepository(IConfiguration config)
        {
            _connectionString = config.GetValue<string>("DatabaseSettings:ConnectionString");
        }

        public NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);
        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = GetConnection();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @productName", new { productName });

            if (coupon == null)
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

            return coupon;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = GetConnection();

            var affected = await connection.ExecuteAsync
            (
                "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@productName, @description, @amount)",
                new { productName = coupon.ProductName, description = coupon.Description, amount = coupon.Amount }
            );

            return affected > 0;
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = GetConnection();

            var affected = await connection.ExecuteAsync
            (
                "UPDATE Coupon SET ProductName=@productName, Description = @description, Amount = @amount WHERE Id = @id",
                new { productName = coupon.ProductName, description = coupon.Description, amount = coupon.Amount, id = coupon.Id }
            );

            return affected > 0;
        }
        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = GetConnection();

            var affected = await connection.ExecuteAsync
            (
                "DELETE FROM Coupon WHERE ProductName = @productName",
                new { productName }
            );

            return affected > 0;
        }
    }
}