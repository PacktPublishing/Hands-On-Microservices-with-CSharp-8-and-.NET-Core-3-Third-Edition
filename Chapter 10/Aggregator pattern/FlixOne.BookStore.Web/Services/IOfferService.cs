using FlixOne.BookStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.Web.Services
{
    public interface IOfferService
    {

        Task<List<OfferViewModel>> ListOffers(Guid dealId, Guid vendorId);

    }
}
