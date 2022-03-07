using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models.Cart
{
    public class OrderItem
    {
        public Restaurant restaurant { get; set; }
        public List<OrderProduct> products { get; set; }
        public string id { get; set; }
    }
}
