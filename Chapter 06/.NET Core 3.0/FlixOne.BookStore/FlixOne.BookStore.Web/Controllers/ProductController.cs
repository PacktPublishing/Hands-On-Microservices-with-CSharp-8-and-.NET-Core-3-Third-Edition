using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using FlixOne.BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Net.Http.Headers;
using FlixOne.BookStore.Web.Utilities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace FlixOne.BookStore.Web.Controllers
{
    //[Authorize]
    public class ProductController : Controller
    {
      
        public async Task<IActionResult> Index()
        {
            AuthenticationResult result = null;
            List<Product> productList = new List<Product>();


            try
            {
                // We came here after sign-in so, we knew the UserId
                string userId = (User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier"))?.Value;

                AuthenticationContext authContext = new AuthenticationContext(AzureAdOptions.Settings.Authority, new NaiveSessionCache(userId, HttpContext.Session));
                ClientCredential credential = new ClientCredential(AzureAdOptions.Settings.ClientId, AzureAdOptions.Settings.ClientSecret);
                result = await authContext.AcquireTokenSilentAsync(AzureAdOptions.Settings.ProductListResourceId, credential, new UserIdentifier(userId, UserIdentifierType.UniqueId));
                return DisplayProductList(result, authContext);

            }
            catch (Exception)
            {
                if (HttpContext.Request.Query["reauth"] != "True")
                {
                    Product product = new Product
                    {
                        Name = "(Sign-in required to view to do list.)"
                    };
                    productList.Add(product);
                    ViewBag.ErrorMessage = "AuthorizationRequired";
                    return View(productList);
                }

                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme);
            }
        }

        private ActionResult DisplayProductList(AuthenticationResult result, AuthenticationContext authContext)
        {
            HttpClient client = new HttpClient();
            string productListApi = AzureAdOptions.Settings.ProductListBaseAddress + "/api/v1/productlist"; //there should be better place for this
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, productListApi);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            HttpResponseMessage response = client.SendAsync(request).Result;
            List<Product> productList = new List<Product>();
            if (response.IsSuccessStatusCode)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                string responseString = response.Content.ReadAsStringAsync().Result;
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(responseString, settings);
                foreach (var product in products)
                {

                    productList.Add(new Product
                    {
                        Name = product.Name,
                        Description = product.Description,
                        Id = product.Id,
                        CreatedBy = product.CreatedBy,
                        CreatedOn = product.CreatedOn

                    });
                }

                return View(productList);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return ProcessUnauthorized(productList, authContext);
            }

            return View("Error");
        }

        private ActionResult ProcessUnauthorized(List<Product> productList, AuthenticationContext authContext)
        {
            var productToken = authContext.TokenCache.ReadItems().Where(a => a.Resource == AzureAdOptions.Settings.ProductListResourceId);
            foreach (TokenCacheItem tci in productToken)
                authContext.TokenCache.DeleteItem(tci);

            ViewBag.ErrorMessage = "UnexpectedError";
            Product product = new Product
            {
                Name = "(No items in list)"
            };
            productList.Add(product);
            return View(productList);
        }
    }
}
