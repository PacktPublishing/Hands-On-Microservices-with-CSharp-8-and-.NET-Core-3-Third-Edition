using FlixOne.BookStore.ProductService.Controllers;
using FlixOne.BookStore.ProductService.Models;
using FlixOne.BookStore.ProductService.Persistence;
using FlixOne.BookStore.ProductService.UnitTests.Fake;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FlixOne.BookStore.ProductService.UnitTests.Services
{
    public class ProductTests
    {
        [Fact]
        public void Get_Returns_ActionResults()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(new ProductData().GetProductList());
            var controller = new ProductController(mockRepo.Object);

            // Act
            var result = controller.GetList();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductViewModel>>(viewResult.Value);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }
    }
}
