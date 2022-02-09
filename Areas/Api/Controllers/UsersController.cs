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
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersService _service = new UsersService(route: "users");

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            List<string> items = new List<string>();
            List<Models.User> users = _service.GetAll();
            
            foreach (var user in users)
            {
                string json = JsonConvert.SerializeObject(user);
                items.Add(json);
            }


            return items as IEnumerable<string>;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<bool> Post([FromBody] User value)
        {
            return Ok(_service.Add(value));
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
