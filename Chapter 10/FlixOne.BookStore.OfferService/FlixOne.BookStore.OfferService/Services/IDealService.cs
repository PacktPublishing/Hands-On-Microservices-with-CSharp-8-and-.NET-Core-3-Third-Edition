using FlixOne.BookStore.OfferService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlixOne.BookStore.OfferService.Services
{
    public interface IDealService
    {
        void Add(Deal deal);
        Task<List<Deal>> GetDeals();
        Task<Deal> GetDeal(string id);
        void Remove(string id);
        void Update(Deal deal);
    }
}
