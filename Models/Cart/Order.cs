using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models.Cart
{
    public class Order
    {
        [FirestoreProperty]
        public string id { get; set; }

        [FirestoreProperty]
        public string userId { get; set; }

        [FirestoreProperty]
        public string deliverId { get; set; }

        [FirestoreProperty]
        public string restaurantId { get; set; }

        public List<OrderItem> orderItems { get; set; }

        [FirestoreProperty]
        public double total { get; set; }

        [FirestoreProperty]
        public string orderStatus { get; set; }

        public Dictionary<string , object> ToMap()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("id", this.id);
            data.Add("userId", this.userId);
            data.Add("deliverId", this.deliverId);
            data.Add("restaurantId", this.restaurantId);
            data.Add("total", this.total);
            data.Add("orderStatus", this.orderStatus);
            return data;
        }
    }
}
