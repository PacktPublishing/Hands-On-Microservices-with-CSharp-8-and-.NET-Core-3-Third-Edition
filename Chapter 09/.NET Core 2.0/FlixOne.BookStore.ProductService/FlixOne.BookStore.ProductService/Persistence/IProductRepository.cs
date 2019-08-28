using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using FlixOne.BookStore.ProductService.Models;

namespace FlixOne.BookStore.ProductService.Persistence
{
    public interface IProductRepository
    {
        IObservable<IEnumerable<Product>> GetAll();
        IObservable<IEnumerable<Product>> GetAll(IScheduler scheduler);
        IObservable<Unit> Remove(Guid productId);
        IObservable<Unit> Remove(Guid productId, IScheduler scheduler);
    }
}