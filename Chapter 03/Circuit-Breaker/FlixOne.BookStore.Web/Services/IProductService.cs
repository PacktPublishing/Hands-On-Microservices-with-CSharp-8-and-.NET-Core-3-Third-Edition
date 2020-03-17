using FlixOne.BookStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlixOne.BookStore.Web.Services
{
    public interface IProductService
    {
        Task<ProductViewModel> GetProduct(Guid id);
        Task<List<ProductViewModel>> ListProducts();
    }
}
