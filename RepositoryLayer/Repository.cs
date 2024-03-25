using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Core;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _db;
        public Repository(AppDbContext db) 
        {
            _db = db;

        }

        public IQueryable<Product> GetProducts()
        {
            return _db.Product;
        }


        public async Task<Product> AddProduct(Product product)
        {
            _db.Product.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                var existingProduct = await _db.Product.FindAsync(product.Id);
                if (existingProduct == null)
                {
                    
                    return false;
                }
                
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                
                await _db.SaveChangesAsync();
                
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }
        public async Task<bool> DeleteProduct(int productId)
        {
            var existingProduct = await _db.Product.FindAsync(productId);
            if (existingProduct != null)
            {
                _db.Product.Remove(existingProduct);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _db.Product.FindAsync(productId);
        }
    }
}
