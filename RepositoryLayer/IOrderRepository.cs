using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetOrders();
        Task<Order> AddOrder(Order order);
        Task<Order> GetOrderById(int orderId);

        Task<bool> UpdateOrderAsync(Order order);

        Task<bool> DeleteOrder(int orderId);
    }
}
