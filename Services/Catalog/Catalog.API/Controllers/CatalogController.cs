using Catalog.API.Entities;
using Catalog.API.Repositories;
using DnsClient.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        #region Dependency Injection

        private readonly IProductRepositor _productRepositor;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepositor productRepositor , ILogger<CatalogController> logger)
        {
            _productRepositor = productRepositor;
            _logger = logger;   
        }
        #endregion

        #region GetProducts
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepositor.GetProducts();

            return Ok(products);
        }
        #endregion

        #region GetProductById

        [HttpGet("{id}", Name = "GetProducts")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsById(string id)
        {
            var product = await _productRepositor.GetProductById(id);

            if (product == null)
            {
                _logger.LogError($"Product by id {id} is not found");
                return NotFound();
            }
            else
                return Ok(product);
        }
        #endregion

        #region GetProducts by Category name 
        [HttpGet("[action]/{category}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsbyCategory(string category)
        {
            var products = await _productRepositor.GetProductByCategory(category);

            return Ok(products);
        }
        #endregion

        #region Create Product
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepositor.CreateProduct(product);

            return CreatedAtAction("GetProducts", new { id = product.Id }, product);
        }
        #endregion

        #region Update Product
        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepositor.UpdateProduct(product));
        }
        #endregion

        #region Delete Product
        [HttpGet("[action]/{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _productRepositor.DeleteProduct(id));
        }
        #endregion
    }
}
