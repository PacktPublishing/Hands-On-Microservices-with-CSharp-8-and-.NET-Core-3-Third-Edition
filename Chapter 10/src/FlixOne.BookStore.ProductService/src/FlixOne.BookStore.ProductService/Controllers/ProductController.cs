using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlixOne.BookStore.ProductService.Models;
using FlixOne.BookStore.ProductService.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace FlixOne.BookStore.ProductService.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("productlist")]
        public async Task<IEnumerable<ProductViewModel>> ListProduct()
        {
            var productModel = await _productRepository.GetAllAsync();
            return productModel.Select(ToProductViewModel).ToList();
        }
        [HttpGet]
        [Route("product/{id}")]
        public async Task<ProductViewModel> Get(string id)
        {
            var order = await _productRepository.GetByAsync(new Guid(id));
            return ToProductViewModel(order);
        }
        private ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                CategoryId = product.CategoryId,
                CategoryDescription = product.Category.Description,
                CategoryName = product.Category.Name,
                ProductDescription = product.Description,
                ProductId = product.Id,
                ProductImage = product.Image,
                ProductName = product.Name,
                ProductPrice = product.Price
            };
        }


        //Rest of code has been removed
    }
}