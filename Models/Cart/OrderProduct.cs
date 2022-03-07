using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models.Cart
{
    public class OrderProduct
    {
        public Product product { get; set; }
        public int quantity { get; set; }
        public string id { get; set; }
    }
}
