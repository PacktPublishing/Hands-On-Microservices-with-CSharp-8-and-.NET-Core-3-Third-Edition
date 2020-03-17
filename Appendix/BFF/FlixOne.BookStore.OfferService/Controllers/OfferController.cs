using FlixOne.BookStore.VendorService.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FlixOne.BookStore.VendorService.Models;

namespace FlixOne.BookStore.offerService.Controllers
{
    [Produces("application/json")]
    [Route("api/offer")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferRepository _offerRepository;

        public OfferController(IOfferRepository offerRepository) => _offerRepository = offerRepository;

        /// <summary>
        /// List all offers
        /// </summary>
        /// <returns>Returns all offers</returns>
        /// <response code="200">Returns all offers</response>
        // GET: api/<controller>
        [HttpGet]
        [Route("list")]
        public ActionResult<IEnumerable<Offer>> Get()
        {
            return _offerRepository.GetAll().ToList();
        }

        /// <summary>
        /// List offer by offer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Offer> Get(string id) => _offerRepository.GetBy(new Guid(id));

        /// <summary>
        /// List offer by product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet("byproduct/{productid}")]
        public ActionResult<Offer> GetByProductId(string productid) => _offerRepository.GetByProductId(new Guid(productid));

        /// <summary>
        /// Add new offer
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public ActionResult Post([FromBody]Offer offer)
        {
            if (offer == null)
                return BadRequest();
            
            _offerRepository.Add(offer);

            return StatusCode(201, new JsonResult(true));
        }

        /// <summary>
        /// Update an existing offer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offer"></param>
        /// <returns></returns>
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody]Offer offer)
        {
            var offerId = new Guid(id);
            if (offer == null || offer.Id != offerId)
                return BadRequest();

            var offerRes = _offerRepository.GetBy(offerId);
            if (offerRes == null)
                return NotFound();

            offerRes.Name = offer.Name;
            offerRes.Description = offer.Description;
            _offerRepository.Update(offerRes);

            return new NoContentResult();
        }

        /// <summary>
        /// Delete an existing offer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var offerId = new Guid(id);
            var offer = _offerRepository.GetBy(offerId);
            if (offer == null)
                return NotFound();

            _offerRepository.Remove(offerId);

            return new NoContentResult();
        }
    }
}
