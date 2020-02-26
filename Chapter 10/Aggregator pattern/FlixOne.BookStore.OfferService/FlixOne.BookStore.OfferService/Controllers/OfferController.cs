using System.Threading.Tasks;
using FlixOne.BookStore.OfferService.Persistence;
using Microsoft.AspNetCore.Mvc;


namespace FlixOne.BookStore.OfferService.Controllers
{

    [Produces("application/json")]
    [Route("api/offer")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferRepository _repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offerRepository"></param>
        public OfferController(IOfferRepository offerRepository)
        {
            _repository = offerRepository;
        }
        /// <summary>
        /// Returns offer by vendor
        /// </summary>
        /// <param name="dealId">Deal Id</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>Returns offer by vendor</returns>
        /// <response code="200">Returns offer by vendor</response>
        [HttpGet("{dealId}/{vendorId}")]
        [ProducesResponseType(typeof(Models.Offer), 200)]
        public async Task<Models.Offer> GetOffer(string dealId, string vendorId)
        {
            var res = await _repository.Get(dealId, vendorId);
            return res;
        }

    }
}
