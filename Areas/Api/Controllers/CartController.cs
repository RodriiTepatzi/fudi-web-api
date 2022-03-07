using fudi_web_api.Areas.Api.Services;
using fudi_web_api.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fudi_web_api.Areas.Api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class CartController : ControllerBase
    {
        CartService _service = new CartService("users");

        // GET: api/<CartController>
        [HttpGet("{id}/cart")]
        public IEnumerable<string> Get(string id)
        {
            List<Cart> carts = _service.GetCart(id);
            List<string> data = new List<string>();

            foreach (var item in carts)
            {
                data.Add(JsonConvert.SerializeObject(item));
            }
            return data as IEnumerable<string>;
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
