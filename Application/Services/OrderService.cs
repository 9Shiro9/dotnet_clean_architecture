﻿using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderItem> _itemsRepository;

        public OrderService(IRepository<Order> ordersRepository, IRepository<OrderItem> itemsRepository)
        {
            _ordersRepository = ordersRepository;
            _itemsRepository = itemsRepository;
        }

        public async Task<bool> AddOrderAsync(Order order, IEnumerable<OrderItem> items)
        {
            bool result = false;
            try
            {
                _ordersRepository.BeginTransaction();

                await _ordersRepository.AddAsync(order);
                await _itemsRepository.AddRangeAsync(items);

                _ordersRepository.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                _ordersRepository.RollbackTransaction();
                result = false;
            }

            return result;
        }

        public Task<string> AutoGeneratedOrderNumber()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
           return await _ordersRepository.GetAllAsync("OrderItems");
        }

        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            return await _ordersRepository.GetByIdAsync(orderId);
        }

        public async Task<Order> GetOrderByOrderNumberAsync(string orderNumber)
        {
            return await _ordersRepository.FirstOrDefaultAsync(x => x.OrderNumber == orderNumber);
        }
    }
}
