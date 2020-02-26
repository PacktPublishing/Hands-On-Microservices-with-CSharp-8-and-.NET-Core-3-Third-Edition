using FlixOne.BookStore.OfferService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.OfferService.Services
{
    public interface IVendorService
    {
        void Add(Vendor vendor);
        Task<List<Vendor>> GetAll();
        Task<Vendor> GetBy(string id);
        void Remove(string id);
        void Update(Vendor vendor);
    }
}
