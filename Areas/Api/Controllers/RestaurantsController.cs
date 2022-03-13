using fudi_web_api.Areas.Api.Services;
using fudi_web_api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fudi_web_api.Areas.Api.Controllers
{
    [Route("api/v1/restaurants")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private RestaurantsService _service = new RestaurantsService("restaurants");

        // Returns all the restaurants
        // GET: api/restaurants
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> items = new List<string>();
            List<Restaurant> restaurants = _service.GetAllRestaurants();

            foreach (var restaurant in restaurants)
            {
                // In case needed. However products will be get separately in the application to avoid errors.
                //restaurant.products = _service.GetProductsByRestaurantId(restaurant.uid);
                string json = JsonConvert.SerializeObject(restaurant);
                items.Add(json);
            }

            return items as IEnumerable<string>;
        }

        // GET api/<RestaurantsController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(string id) => await _service.Get(id);

        [HttpGet("category/{id}")]
        public  IEnumerable<string> GetCategory(string id) {
            List<string> items = new List<string>();
            List<Restaurant> restaurants = _service.RestaurantsByCategory(new List<string> { id });

            foreach (var restaurant in restaurants)
            {
                // In case needed. However products will be get separately in the application to avoid errors.
                //restaurant.products = _service.GetProductsByRestaurantId(restaurant.uid);
                string json = JsonConvert.SerializeObject(restaurant);
                items.Add(json);
            }

            return items as IEnumerable<string>;
        }

        // POST api/<RestaurantsController>
        [HttpPost]
        public ActionResult<bool> Post([FromBody] Restaurant restaurant) => Ok(_service.AddRestaurant(restaurant));

        // PUT api/<RestaurantsController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Restaurant restaurant) => Ok(_service.Update(id, restaurant));

        // DELETE api/<RestaurantsController>/5
        [HttpDelete("{id}")]
        public void Delete(string id) => Ok(_service.Delete(id));
    }
}
