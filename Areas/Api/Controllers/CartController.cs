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
    }
}
