using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Core
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {

                Id = 1,
                Name = "Books",
                Price = 500,
                Description = "Nice Book"

            });

            modelBuilder.Entity<Product>().HasData(new Product
            {

                Id = 2,
                Name = "Papers",
                Price = 5000,
                Description = "Empty Papers"

            });

            modelBuilder.Entity<Order>().HasData(new Order
            {

                Id = 1,
                ProductId = 1,
                OrderBy = "Sindhu"

            });

            modelBuilder.Entity<Order>().HasData(new Order
            {

                Id = 2,
                ProductId = 1,
                OrderBy = "John"

            });


            modelBuilder.Entity<Order>().HasData(new Order
            {

                Id = 3,
                ProductId = 1,
                OrderBy = "Mark"

            });

            modelBuilder.Entity<Order>().HasData(new Order
            {

                Id = 4,
                ProductId = 2,
                OrderBy = "Carlin"

            });

            modelBuilder.Entity<Order>().HasData(new Order
            {

                Id = 5,
                ProductId = 2,
                OrderBy = "Jack"

            });
        }
    }
}
