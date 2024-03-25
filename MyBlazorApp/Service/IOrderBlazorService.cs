using ViewModels;

namespace MyBlazorApp.Service
{
    public interface IOrderBlazorService
    {
        Task<List<OrderVM>> GetAll();
        Task<int> Add(OrderVM order);
        Task<OrderVM> GetOrderById(int productId);

         Task Update(OrderVM updateOrder);

        Task Delete(int orderId);
    }
}
