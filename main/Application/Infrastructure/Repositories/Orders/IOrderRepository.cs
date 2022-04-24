﻿using Application.Infrastructure.Entities;
using Application.Infrastructure.SeedWork;

namespace Application.Infrastructure.Repositories.Orders
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<Order?> GetOrderWithOrderState(int orderId);
        Task<Order?> SearchOrderByName(string name);
        Task<Order?> SearchOrderByDate(DateTime date);
    }
}
