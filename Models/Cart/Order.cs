using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models.Cart
{
    public class Order
    {
        public string id { get; set; }
        public string userId { get; set; }
        public string deliverId { get; set; }
        public string restaurantId { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public string total { get; set; }
        public string orderStatus { get; set; }
    }
}
