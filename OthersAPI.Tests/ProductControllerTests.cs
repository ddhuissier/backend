using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Repositories;
using Xunit;

namespace OthersAPI.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> _repositoryStub = new Mock<IProductRepository>();
        private ProductsController controller;

        [Fact]
        public async Task CreateProduct_WithProductToCreate_ReturnsCreatedProduct()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.SetupGet(x => x.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext = mockControllerContext.Object;

            // Arrange
            var product = CreateItem();

            controller = new ProductsController(_repositoryStub.Object);

            // Act
            var result = await controller.PostProduct(product);

            // Asserts
            var resultItem = (result.Result as CreatedAtActionResult).Value as Product;
            resultItem.Id.Should().BeGreaterThan(0);
            product.Should().BeEquivalentTo(
                resultItem,
                options => options.ComparingByMembers<Product>());
        }


        private Product CreateItem()
        {
            return new Product
            {
               // Id = new Random().Next(),
                Name = Guid.NewGuid().ToString(),
                Price = new Random().Next(2000)
            };
        }
    }
}
