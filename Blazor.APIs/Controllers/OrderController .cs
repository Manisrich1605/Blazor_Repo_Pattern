using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.Models;
using ViewModels;

namespace Blazor.APIs.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _business;

        public OrderController(IOrderBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        [Route("GetOrders")]


        public async Task<ActionResult<List<OrderVM>>> GetOrders()
        {
            var orders = await _business.GetOrders();

            return Ok(orders);
        }

        [HttpPost]
        [Route("AddOrder")]
        public async Task<ActionResult<int>> AddOrder(OrderVM order)
        {
            var newOrderId = await _business.AddOrder(order);

            return newOrderId;
        }

        [HttpGet]
        [Route("GetOrder/{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            try
            {
                var order = await _business.GetOrderById(id);
                if (order == null)
                {
                    return NotFound(); 
                }
                
                return Ok(order); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("EditOrder/{orderId}")]
        public async Task<ActionResult> UpdateOrder(int orderId, [FromBody] OrderVM updatedOrder)
        {
            if (orderId <= 0)
            {
                return BadRequest("Invalid orderId");
            }
            updatedOrder.Id = orderId;
            var success = await _business.UpdateOrderAsync(updatedOrder);
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
        [Route("DeleteOrder/{orderId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            var success = await _business.DeleteOrder(orderId);
            if (success)
                return NoContent();
            else
                return NotFound();
        }

    }
}
