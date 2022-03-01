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
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryService _service = new CategoryService("categories");

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> categoriesData = new List<string>();
            List<Category> categories = _service.GetAll();
            foreach (Category category in categories)
            {
                categoriesData.Add(JsonConvert.SerializeObject(category));
            }

            return categoriesData as IEnumerable<string>;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
