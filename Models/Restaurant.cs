using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models
{
    public class Restaurant
    {
        public string uid { get; set; }
        public string restaurantName { get; set; }
        public string restaurantAddress { get; set; }
        public string restaurantUrl { get; set; }
        public string restaurantSlogan { get; set; }
        public string stars { get; set; }
        public string cost { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public string startDate { get; set; }
        public List<Product> products { get; set; }
    }
}
