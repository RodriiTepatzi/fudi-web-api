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
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        ProductsService _service = new ProductsService("restaurants");

        // GET api/products/{restaurantId}
        [HttpGet("{id}")]
        public async Task<IEnumerable<string>> Get(string id)
        {
            List<string> items = new List<string>();
            List<Product> products = _service.GetAllProducts(id);

            foreach (var product in products)
            {
                string json = JsonConvert.SerializeObject(product);
                items.Add(json);
            }

            return items as IEnumerable<string>;
        }

        // POST api/products/{restaurantId}
        [HttpPost("{id}")]
        public ActionResult<bool> Post(string id, [FromBody] Product product) => Ok(_service.AddProductToRestaurant(id, product));

        // PUT api/products/{restaurantId}
        [HttpPut("{id}")]
        public ActionResult<bool> Put(string id, [FromBody] Product product) => Ok(_service.UpdateProductInRestaurant(id, product));

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
