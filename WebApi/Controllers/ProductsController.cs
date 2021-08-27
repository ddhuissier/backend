using WebApi.Models;
using WebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            return await _productRepository.Get(id);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Product>>PostProducts([FromBody] Product Product)
        {
            var newProduct = await _productRepository.Create(Product);
            return CreatedAtAction(nameof(GetProducts), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> PutProducts(int id, [FromBody] Product Product)
        {
            if(id != Product.Id)
            {
                return BadRequest();
            }

            await _productRepository.Update(Product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete (int id)
        {
            var productToDelete = await _productRepository.Get(id);
            if (productToDelete == null)
                return NotFound();

            await _productRepository.Delete(productToDelete.Id);
            return NoContent();
        }
    }
}
