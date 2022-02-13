using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models
{
    public class Product
    {
        public string productName { get; set; }
        public string productDescription { get; set; }
        public double productPrice { get; set; }
        public string productUrl { get; set; }
        public string productRating { get; set; }
        public string productLikes { get; set; }
        public string productUnit { get; set; } 
    }
}
