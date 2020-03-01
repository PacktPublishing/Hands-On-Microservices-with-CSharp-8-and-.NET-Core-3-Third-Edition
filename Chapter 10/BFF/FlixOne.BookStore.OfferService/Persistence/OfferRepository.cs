using System;
using System.Collections.Generic;
using System.Linq;
using FlixOne.BookStore.VendorService.Contexts;

namespace FlixOne.BookStore.VendorService.Persistence
{
    public class OfferRepository : IOfferRepository
    {
        private readonly OfferContext _context;

        public OfferRepository(OfferContext context) => _context = context;

        public void Add(Models.Offer offer)
        {
            _context.Offers.Add(offer);
            _context.SaveChanges();
        }

        public List<Models.Offer> GetAll() => _context.Offers.ToList();

        public Models.Offer GetBy(Guid id) => _context.Offers.FirstOrDefault(a => a.Id == id);
        public Models.Offer GetByProductId(Guid id) => _context.Offers.FirstOrDefault(a => a.ProductId == id);

        public void Remove(Guid id)
        {
            var offer = GetBy(id);
            _context.Remove(offer);
            _context.SaveChanges();
        }

        public void Update(Models.Offer offer)
        {
            _context.Update(offer);
            _context.SaveChanges();
        }
    }
}