using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BusinessLayer
{
    public interface IBusiness
    {
        Task<List<ProductVM>> GetProducts();
        Task<int> AddProduct(ProductVM product);
        Task<bool> UpdateProductAsync(ProductVM updatedProduct);
        Task<bool> DeleteProduct(int productId);
        Task<ProductVM> GetProductById(int productId);
    }
}
