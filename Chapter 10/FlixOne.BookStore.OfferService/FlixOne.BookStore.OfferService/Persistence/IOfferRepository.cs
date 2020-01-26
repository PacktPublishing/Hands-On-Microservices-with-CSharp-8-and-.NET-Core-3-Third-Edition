using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlixOne.BookStore.OfferService.Persistence
{
    public interface IOfferRepository
    {
        Task<List<Models.Offer>> Get();
        Task<Models.Offer> Get(string dealId, string vendorId);
    }
}
