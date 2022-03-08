using Google.Cloud.Firestore;
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

        [FirestoreProperty]
        public string id { get; set; }

        public Dictionary<string, object> ToMap()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("id", this.id);
            return data;
        }
    }
}
