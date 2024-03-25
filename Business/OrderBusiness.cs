using AutoMapper;
using RepositoryLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public OrderBusiness(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<List<OrderVM>> GetOrders()
        {
            var data = await _repo.GetOrders().ToListAsync();

            var orders = _mapper.Map<List<Order>, List<OrderVM>>(data);
            return orders;
        }

        public async Task<int> AddOrder(OrderVM order)
        {
            var data = _mapper.Map<OrderVM, Order>(order);
            var result = _repo.AddOrder(data);
            return result.Id;
        }


        public async Task<OrderVM> GetOrderById(int productId)
        {

            var order = await _repo.GetOrders().Where(x => x.Id == productId).FirstOrDefaultAsync();
            var orderVm = _mapper.Map<Order, OrderVM>(order);
            return orderVm;
        }

        public async Task<bool> UpdateOrderAsync(OrderVM updatedOrder)
        {
            try
            {
                var data = _mapper.Map<OrderVM, Order>(updatedOrder);
                var success = await _repo.UpdateOrderAsync(data);
                return success;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            return await _repo.DeleteOrder(orderId);
        }


    }
}