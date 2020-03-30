using FlixOne.BookStore.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlixOne.BookStore.Web.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAsync();
    }
}
