using System;
using System.Collections.Generic;
using FlixOne.BookStore.VendorService.Models;

namespace FlixOne.BookStore.VendorService.Persistence
{
    public interface IVendorRepository
    {
        void Add(Models.Vendor vendor);

        List<Models.Vendor> GetAll();

        Models.Vendor GetBy(Guid id);

        void Remove(Guid id);

        void Update(Models.Vendor vendor);
    }
}