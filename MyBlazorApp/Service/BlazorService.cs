using MudBlazor;
using MyBlazorApp.Service;
using System.Net.Http.Json;
using ViewModels;
using System.Threading.Tasks;

public class BlazorService : IBlazorService
{
    private readonly HttpClient _httpClient;
    private List<ProductVM> products;
    
    public BlazorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        products = new List<ProductVM>();
    }
    public async Task<List<ProductVM>> GetAll()
    {

        var fetchedProducts = await _httpClient.GetFromJsonAsync<List<ProductVM>>("https://localhost:44383/api/products/GetProducts");

        products.Clear();
        products.AddRange(fetchedProducts);
        return products;
    }
    public async Task<int> Add(ProductVM product)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:44383/api/products/AddProduct", product);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }
    public async Task Delete(int productId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:44383/api/products/DeleteProduct/{productId}");
            response.EnsureSuccessStatusCode();

            var deletedProduct = products.FirstOrDefault(p => p.Id == productId);
            if (deletedProduct != null)
            {
                products.Remove(deletedProduct);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product: {ex.Message}");
        }
    }
    public async Task<ProductVM> GetProductById(int productId)
    {
        var product = await _httpClient.GetFromJsonAsync<ProductVM>($"https://localhost:44383/api/products/GetProduct/{productId}");
        return product;
    }
    public async Task Update(ProductVM updatedProduct)
    {
        try
        {
            int productId = updatedProduct.Id;
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:44383/api/products/EditProduct/{productId}", updatedProduct);
            response.EnsureSuccessStatusCode();

            var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (existingProduct != null)
            {

                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Description = updatedProduct.Description;

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product: {ex.Message}");
        }
    }
}