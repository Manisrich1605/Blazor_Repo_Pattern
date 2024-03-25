using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BusinessLayer
{
    public class Business : IBusiness
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        public Business(IRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ProductVM>> GetProducts()
        {
            var data = await _repo.GetProducts().ToListAsync();

            var products =  _mapper.Map<List<Product>, List<ProductVM>>(data);

            return products;

        }

        public async Task<int> AddProduct(ProductVM product)
        {
            var data = _mapper.Map<ProductVM, Product>(product);

            var result = _repo.AddProduct(data);

            return result.Id;
        }
        public async Task<bool> UpdateProductAsync(ProductVM updatedProduct)
        {
            try
            {
                var data = _mapper.Map<ProductVM, Product>(updatedProduct);
                var success = await _repo.UpdateProductAsync(data);
                return success;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
        public async Task<bool> DeleteProduct(int productId)
        {
            return await _repo.DeleteProduct(productId);
        }
        public async Task<ProductVM> GetProductById(int productId)
        {

            var product = await _repo.GetProducts().Include(x=>x.Orders).Where(x=>x.Id==productId).FirstOrDefaultAsync();
            
            var productVm = _mapper.Map<Product, ProductVM>(product);
            return productVm;
        }
    }
}
