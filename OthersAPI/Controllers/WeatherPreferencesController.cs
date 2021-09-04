using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<WeatherPreferencesController> logger; 

        public WeatherPreferencesController(
            IWeatherPrefRepository repo,
            ILogger<WeatherPreferencesController> logger
        )
        {
            this.repo = repo;
            this.logger = logger;
        }
        // GET: api/<WeatherPreferencesController>
        [HttpGet]
        public IEnumerable<WeatherPreference> Get() 
        { 
            var items =repo.Get();
            logger.LogInformation($"Get WeatherPreferences: {DateTime.Now.ToString("hh:mm:ss")}: items Count: {items.Count()} ");
            return items;
         }

        // GET api/<WeatherPreferencesController>/5
        [HttpGet("{id}")]
        public ActionResult<WeatherPreference> Get(Guid id) 
        {
            var item = repo.Get(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
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
