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
    [Authorize]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoresController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet]
        // [Authorize]
        public async Task<IEnumerable<Store>> GetStores()
        {
            return await _storeRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStores(int id)
        {
            return await _storeRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Store>>PostStores([FromBody] Store Store)
        {
            var newStore = await _storeRepository.Create(Store);
            return CreatedAtAction(nameof(GetStores), new { id = newStore.Id }, newStore);
        }

        [HttpPut]
        public async Task<ActionResult> PutStores(int id, [FromBody] Store Store)
        {
            if(id != Store.Id)
            {
                return BadRequest();
            }

            await _storeRepository.Update(Store);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var storeToDelete = await _storeRepository.Get(id);
            if (storeToDelete == null)
                return NotFound();

            await _storeRepository.Delete(storeToDelete.Id);
            return NoContent();
        }
    }
}
