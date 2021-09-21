using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.Contract.V1;
using WebApi.Models;

namespace WebApi.IntegrationTests
{
    public class ProductControllerTests : IntegrationTests
    {
        public async Task GetAll_WithoutAnyProduct_ReturnsEmptyResponse()
        {
            //// Arrange
            //await AuthenticateAsync();

            //// Act
            //var response = await httpClient.GetAsync(ApiRoutes.Products.GetProducts);

            //// Assert
            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            //(await response.Content.ReadAsAsync<List<Product>>()).Should().BeEmpty();
        }
    }
}
