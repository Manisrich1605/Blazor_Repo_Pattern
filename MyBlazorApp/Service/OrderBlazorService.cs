using RepositoryLayer.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using ViewModels;
using static MudBlazor.CategoryTypes;
using RepositoryLayer;
using MyBlazorApp.Pages;
//using RepositoryLayer.Migrations;

namespace MyBlazorApp.Service
{
    public class OrderBlazorService : IOrderBlazorService
    {
        private readonly HttpClient _httpClient;
        private List<OrderVM> orders;


        public OrderBlazorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<OrderVM>> GetAll()
        {
            var orders = await _httpClient.GetFromJsonAsync<List<OrderVM>>("https://localhost:44383/api/orders/GetOrders");

            return orders;

        }

        public async Task<int> Add(OrderVM order)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44383/api/orders/AddOrder", order);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }


        public async Task<OrderVM> GetOrderById(int productId)
        {
            var order = await _httpClient.GetFromJsonAsync<OrderVM>($"https://localhost:44383/api/orders/GetOrder/{productId}");
            return order;
        }

        public async Task Update(OrderVM updatedOrder)
        {
            try
            {
                int OrderId = updatedOrder.Id;
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44383/api/orders/EditOrder/{OrderId}", updatedOrder);
                response.EnsureSuccessStatusCode();

                var existingOrder = orders.FirstOrDefault(p => p.Id == updatedOrder.Id);
                if (existingOrder != null)
                {

                    existingOrder.ProductId = updatedOrder.ProductId;
                    existingOrder.OrderBy = updatedOrder.OrderBy;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order: {ex.Message}");
            }



        }

        public async Task Delete(int orderId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:44383/api/orders/DeleteOrder/{orderId}");
                response.EnsureSuccessStatusCode();

                var deletedOrder = orders.FirstOrDefault(p => p.Id == orderId);
                if (deletedOrder != null)
                {
                    orders.Remove(deletedOrder);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting order: {ex.Message}");
            }
        }

    }
}
