using System;
using System.Collections.Generic;
using System.Linq;
using FlixOne.BookStore.VendorService.Common;
using FlixOne.BookStore.VendorService.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace FlixOne.BookStore.VendorService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorController(IVendorRepository vendorRepository) => _vendorRepository = vendorRepository;

        /// <summary>
        /// List all vendors
        /// </summary>
        /// <returns>Returns all vendors</returns>
        /// <response code="200">Returns all vendors</response>
        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Models.VendorViewModel>), 200)]
        public ActionResult<IEnumerable<Models.VendorViewModel>> Get() => _vendorRepository.GetAll().ToViewModel().ToList();

        /// <summary>
        /// List vendor by vendor id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Models.VendorViewModel> Get(string id) => _vendorRepository.GetBy(new Guid(id)).ToViewModel();

        /// <summary>
        /// Add new vendor
        /// </summary>
        /// <param name="vendorViewModel"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Models.VendorViewModel vendorViewModel)
        {
            if (vendorViewModel == null)
                return BadRequest();
            var vendorModel = Common.Transpose.ToModel(vendorViewModel);

            _vendorRepository.Add(vendorModel);

            return StatusCode(201, new JsonResult(true));
        }

        /// <summary>
        /// Update an existing vendor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vendorViewModel"></param>
        /// <returns></returns>
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody]Models.VendorViewModel vendorViewModel)
        {
            var vendorId = new Guid(id);
            if (vendorViewModel == null || vendorViewModel.Id != id)
                return BadRequest();

            var vendor = _vendorRepository.GetBy(vendorId);
            if (vendor == null)
                return NotFound();

            vendor.Name = vendorViewModel.Name;
            vendor.Description = vendorViewModel.Description;
            _vendorRepository.Update(vendor);

            return new NoContentResult();
        }

        /// <summary>
        /// Delete an existing vendor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var vendorId = new Guid(id);
            var vendor = _vendorRepository.GetBy(vendorId);
            if (vendor == null)
                return NotFound();

            _vendorRepository.Remove(vendorId);

            return new NoContentResult();
        }
    }
}
