using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlixOne.BookStore.OrderService.Models;

namespace FlixOne.BookStore.OrderService.Clients
{
    public interface IProductClient
    {
        Task<IEnumerable<ProductDetail>> GetProductDetailAsync(Guid productId);
    }
}