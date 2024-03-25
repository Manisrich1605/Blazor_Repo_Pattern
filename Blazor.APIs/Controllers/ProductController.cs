using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.Models;
using ViewModels;

namespace Blazor.APIs.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBusiness _business;

        public ProductController(IBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        [Route("GetProducts")]


        public async Task<ActionResult<List<ProductVM>>> GetProducts()
        {
            var products = await _business.GetProducts();

            return Ok(products);
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult<int>> AddProduct(ProductVM product)
        {
            var newProductId = await _business.AddProduct(product);

            return newProductId;
        }
        [HttpPut]
        [Route("EditProduct/{productId}")]
        public async Task<ActionResult> UpdateProduct(int productId, [FromBody] ProductVM updatedProduct)
        {
            if (productId <= 0)
            {
                return BadRequest("Invalid productId");
            }
            updatedProduct.Id = productId; 
            var success = await _business.UpdateProductAsync(updatedProduct);
            if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("DeleteProduct/{productId}")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var success = await _business.DeleteProduct(productId);
            if (success)
                return NoContent(); 
            else
                return NotFound(); 
        }

        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _business.GetProductById(id);
                if (product == null)
                {
                    return NotFound(); 
                }
                
                return Ok(product); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
