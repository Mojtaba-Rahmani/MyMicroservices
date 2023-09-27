using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> ProductCollection)
        {
            bool existProduct = ProductCollection.Find(p => true).Any();
            if (!existProduct)
            {
                ProductCollection.InsertManyAsync(GetSeedData());
            }
        }

        private static IEnumerable<Product> GetSeedData()
        {
            return new List<Product>()
          {
              new Product()
              {
                  Id = "",
                  Name ="",
                  Category ="",
                  Description ="",
                  ImageName =  "",
                  Price =1,
                  Summery=""
              },
                 new Product()
              {
                  Id = "",
                  Name ="",
                  Category ="",
                  Description ="",
                  ImageName =  "",
                  Price =2,
                  Summery=""
              }
          };
        }
    }
}
