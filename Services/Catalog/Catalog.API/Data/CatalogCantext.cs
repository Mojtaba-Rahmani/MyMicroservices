using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogCantext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
        public CatalogCantext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaeSettings:ConnectionStrings"));
            // if db exist return it , when not exist created it
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaeSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaeSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

    }
}
