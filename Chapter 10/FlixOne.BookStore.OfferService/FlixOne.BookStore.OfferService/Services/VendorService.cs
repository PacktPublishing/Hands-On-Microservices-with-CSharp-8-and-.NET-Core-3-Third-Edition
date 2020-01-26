using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FlixOne.BookStore.OfferService.Common;
using FlixOne.BookStore.OfferService.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FlixOne.BookStore.OfferService.Services
{
    public class VendorService : IVendorService
    {
        private HttpClient _httpClient;
        private readonly IOptions<AppSettings> _settings;
        private readonly string _baseURL;
        public VendorService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;

            _baseURL = $"{settings.Value.VendorUrl}/api/vendor";
        }
        public async Task<List<Vendor>> GetAll()
        {
            var endPoint = API.Vendor.GetList(_baseURL);
            var resString = await _httpClient.GetStringAsync(endPoint);
            var response = JsonConvert.DeserializeObject<List<Vendor>>(resString);
            return response;
        }

        public async Task<Vendor> GetBy(string id)
        {
            var endPoint = API.Vendor.GetVendor(_baseURL, id);
            var resString = await _httpClient.GetStringAsync(endPoint);
            var response = JsonConvert.DeserializeObject<Vendor>(resString);
            return response;
        }
        public void Add(Vendor vendor)
        {
            throw new NotImplementedException();
        }
        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Vendor vendor)
        {
            throw new NotImplementedException();
        }
    }
}
