using System;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FlixOne.BookStore.OfferService.Common;
using FlixOne.BookStore.OfferService.Models;

namespace FlixOne.BookStore.OfferService.Services
{
    public class DealService : IDealService
    {
        private HttpClient _httpClient;
        private readonly IOptions<AppSettings> _settings;
        private readonly string _baseURL;
        public DealService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;

            _baseURL = $"{settings.Value.DealUrl}/api/deal";
        }

        public async Task<List<Deal>> GetDeals()
        {
            var endPoint = API.Deal.GetAllDeals(_baseURL);
            var resString = await _httpClient.GetStringAsync(endPoint);
            var response = JsonConvert.DeserializeObject<List<Deal>>(resString);
            return response;
        }


        public async Task<Deal> GetDeal(string id)
        {
            var endPoint = API.Deal.GetDeal(_baseURL, id);
            var resString = await _httpClient.GetStringAsync(endPoint);
            var response = JsonConvert.DeserializeObject<Deal>(resString);
            return response;
        }
        public void Add(Deal deal)
        {
            throw new NotImplementedException();
        }
        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Deal deal)
        {
            throw new NotImplementedException();
        }
    }
}
