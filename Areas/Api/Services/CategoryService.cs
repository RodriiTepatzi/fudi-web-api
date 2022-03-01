using fudi_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Areas.Api.Services
{
    public class CategoryService : BaseRepository<Category>
    {
        public CategoryService(string route) : base(route)
        {

        }
    }
}
