using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BusinessLayer
{
    public interface IOrderBusiness
    {
        Task<List<OrderVM>> GetOrders();
        Task<int> AddOrder(OrderVM order);
        Task<OrderVM> GetOrderById(int productId);
        Task<bool> UpdateOrderAsync(OrderVM updatedOrder);
        Task<bool> DeleteOrder(int orderId);
    }
}
