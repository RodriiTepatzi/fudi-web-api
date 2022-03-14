using fudi_web_api.Areas.Api.Services;
using fudi_web_api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fudi_web_api.Areas.Api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersService _service = new UsersService(route: "users");

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            List<string> items = new List<string>();
            List<User> users = _service.GetAll();
            
            foreach (var user in users)
            {
                string json = JsonConvert.SerializeObject(user);
                items.Add(json);
            }


            return items as IEnumerable<string>;
        }

        [HttpGet("telephone/{number}")]
        public async Task<string> GetByNumber(string number) {
            string newNumber = "";
            if (number.Length == 12)
            {
                newNumber = "+" + Regex.Replace(number, @"(\d{2})(\d{3})(\d{3})(\d{4})", "$1 $2-$3-$4").ToString();
            }
            else if (number.Length == 11)
            {
                newNumber = "+" + Regex.Replace(number, @"(\d{1})(\d{3})(\d{3})(\d{4})", "$1 $2-$3-$4").ToString();
            }
            
            List<string> items = new List<string>();
            List<User> users = _service.GetUserByNumber(new List<string> { newNumber });
            foreach (var user in users)
            {
                string json = JsonConvert.SerializeObject(user);
                items.Add(json);
            }

            return items.Count > 0 ? items[0] as string : null;
        }

        [HttpGet("username/{username}")]
        public async Task<string> GetByUsername(string username)
        {

            List<string> items = new List<string>();
            List<User> users = _service.GetUserByUsername(new List<string> { username });
            foreach (var user in users)
            {
                string json = JsonConvert.SerializeObject(user);
                items.Add(json);
            }

            return items.Count > 0 ? items[0] as string : null;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(string id) => await _service.GetUserById(id);
        

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<bool> Post([FromBody] User value) => Ok(_service.AddUser(value));
        

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] User value) => Ok(_service.Update(id, value));


        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id) => Ok(_service.Delete(id));
    }
}