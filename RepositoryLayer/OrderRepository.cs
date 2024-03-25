using RepositoryLayer.Core;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryLayer
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db)
        {
            _db = db;

        }

        public IQueryable<Order> GetOrders()
        {
            return _db.Order;
        }


        public async Task<Order> AddOrder(Order order)
        {
            _db.Order.Add(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _db.Order.FindAsync(orderId);
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            try
            {
                var existingOrder = await _db.Order.FindAsync(order.Id);
                if (existingOrder == null)
                {

                    return false;
                }

                existingOrder.ProductId = order.ProductId;
                existingOrder.OrderBy = order.OrderBy;
                

                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            var existingOrder = await _db.Order.FindAsync(orderId);
            if (existingOrder != null)
            {
                _db.Order.Remove(existingOrder);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
