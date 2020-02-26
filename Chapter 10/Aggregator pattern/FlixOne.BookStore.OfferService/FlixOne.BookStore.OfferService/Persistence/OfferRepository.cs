using FlixOne.BookStore.OfferService.Models;
using FlixOne.BookStore.OfferService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlixOne.BookStore.OfferService.Persistence
{
    public class OfferRepository : IOfferRepository
    {
        private readonly IDealService _dealService;
        private readonly IVendorService _vendorService;

        public OfferRepository(IDealService dealService, IVendorService vendorService)
        {
            _dealService = dealService;
            _vendorService = vendorService;
        }

        public async Task<Models.Offer> Get(string dealId, string vendorId)
        {
            Deal deal = await _dealService.GetDeal(dealId);
            Vendor vendor = await _vendorService.GetBy(vendorId);
            var offer = new Models.Offer(deal, vendor);
            return offer.Get();
        }

        public Task<List<Models.Offer>> Get()
        {
            throw new System.NotImplementedException();
        }
    }
}
