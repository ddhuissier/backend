using WebApi.Models;
using WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApi.Contract.V1;

namespace WebApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet(ApiRoutes.Products.GetProducts)]
        [Authorize]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.Get();
        }

        [HttpGet(ApiRoutes.Products.GetProduct)]
        public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
        {
            return await _productRepository.Get(id);
        }

        [HttpPost(ApiRoutes.Products.PostProduct)]
        [Authorize]
        public async Task<ActionResult<Product>>PostProduct([FromBody] Product product)
        {
            var newProduct = await _productRepository.Create(product);
            return CreatedAtAction(nameof(GetProducts), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut(ApiRoutes.Products.PutProduct)]
        [Authorize]
        public async Task<ActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }

            await _productRepository.Update(product);

            return NoContent();
        }

        [HttpDelete(ApiRoutes.Products.DeleteProduct)]
        [Authorize]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productToDelete = await _productRepository.Get(id);
            if (productToDelete == null)
                return NotFound();

            await _productRepository.Delete(productToDelete.Id);
            return NoContent();
        }
    }
}
