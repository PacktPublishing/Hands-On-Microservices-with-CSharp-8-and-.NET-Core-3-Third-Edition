using System;
using System.Collections.Generic;
using System.Linq;
using FlixOne.BookStore.VendorService.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.VendorService.Persistence
{
    public class VendorRepository : IVendorRepository
    {
        private readonly VendorContext _context;

        public VendorRepository(VendorContext context) => _context = context;

        public void Add(Models.Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            _context.SaveChanges();
        }

        public List<Models.Vendor> GetAll() => _context.Vendors.Include(c => c.Addresses).ToList();

        public Models.Vendor GetBy(Guid id)
        {
            return _context.Vendors.Include(c => c.Addresses).FirstOrDefault(a => a.Id == id);
        }

        public void Remove(Guid id)
        {
            var vendor = GetBy(id);
            _context.Remove(vendor);
            _context.SaveChanges();
        }

        public void Update(Models.Vendor vendor)
        {
            _context.Update(vendor);
            _context.SaveChanges();
        }
    }
}