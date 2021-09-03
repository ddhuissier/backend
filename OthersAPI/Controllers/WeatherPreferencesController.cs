using Microsoft.AspNetCore.Mvc;
using OthersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OthersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherPreferencesController : ControllerBase
    {
        private readonly IWeatherPrefRepository repo;

        public WeatherPreferencesController(IWeatherPrefRepository repo)
        {
            this.repo = repo;
        }
        // GET: api/<WeatherPreferencesController>
        [HttpGet]
        public IEnumerable<WeatherPreference> Get() 
        {
            return repo.Get();
         }

        // GET api/<WeatherPreferencesController>/5
        [HttpGet("{id}")]
        public WeatherPreference Get(Guid id) 
        {
            return repo.Get(id);
        }

        // POST api/<WeatherPreferencesController>
        [HttpPost]
        public void Post([FromBody] WeatherPreference value)
        {
            repo.Create(value);
        }

        // PUT api/<WeatherPreferencesController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] WeatherPreference value)
        {
            repo.Update(id,value);
        }

        // DELETE api/<WeatherPreferencesController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            repo.Delete(id);
        }
    }
}
