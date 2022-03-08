using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models.Cart
{
    public class OrderProduct
    {
        public Product product { get; set; }

        [FirestoreProperty]
        public int quantity { get; set; }

        [FirestoreProperty]
        public string id { get; set; }

        public Dictionary<string, object> ToMap()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("quantity", quantity);
            data.Add("id", id);
            return data;
        }
    }
}
