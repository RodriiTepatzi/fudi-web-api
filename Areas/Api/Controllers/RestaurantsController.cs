using fudi_web_api.Areas.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fudi_web_api.Areas.Api.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        // Returns all the restaurants
        // GET: api/restaurants
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "a", ""};
        }

        // GET api/<RestaurantsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RestaurantsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RestaurantsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RestaurantsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
