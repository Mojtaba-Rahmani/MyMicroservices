using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Threading;

namespace Discount.Api.Extension
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                // this is configuration in startup
                // exampl:
                var logger = services.GetRequiredService<ILogger<TContext>>();

                // Migration Database

                try
                {
                    logger.LogInformation("migrating postgtresql database");
                    
                    using var connection = new NpgsqlConnection(
                        configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection,
                    };
                    command.CommandText = "Drop Table If Exists Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"Create Table Coupon(Id Seryal Primary Key,
                                                                PrudoctName Varchar(200) Not Null,
                                                                Description Text,
                                                                Amount Int)";
                    command.ExecuteNonQuery();

                    // seed data

                    command.CommandText = "Insert Into Coupon(PrudoctName,Description,Amount) Values('IPhone x1','Iphone discount',150)";
                    command.ExecuteNonQuery();

                    command.CommandText = "Insert Into Coupon(PrudoctName,Description,Amount) Values('Sumsung 10','Sumsung discount',250)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("migration has been complated");

                }
                catch (NpgsqlException ex)
                {
                    logger.LogError("an error has been ocured");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }
            return host;
        }
    }
}
