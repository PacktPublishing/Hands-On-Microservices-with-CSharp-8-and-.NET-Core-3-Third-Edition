using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.VendorService.Persistence
{
    public interface IOfferRepository
    {
        void Add(Models.Offer vendor);

        List<Models.Offer> GetAll();

        Models.Offer GetBy(Guid id);
        Models.Offer GetByProductId(Guid id);

        void Remove(Guid id);

        void Update(Models.Offer vendor);
    }
}