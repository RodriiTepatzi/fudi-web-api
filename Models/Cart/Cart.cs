using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models.Cart
{
    public class Cart
    {
        [FirestoreProperty]
        public string uid { get; set; }

        [FirestoreProperty]
        public int total { get; set; }

        [FirestoreProperty]
        public DateTime openedDate { get; set; }

        [FirestoreProperty]
        public DateTime closedDate { get; set; }

        public List<Order> orders { get; set; }

        public Dictionary<string, object> ToMap()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("uid", this.uid);
            data.Add("total", this.total);
            data.Add("openedDate", Timestamp.FromDateTime(DateTime.SpecifyKind(this.openedDate, DateTimeKind.Utc)));
            data.Add("closedDate", Timestamp.FromDateTime(DateTime.SpecifyKind(this.closedDate, DateTimeKind.Utc)));
            return data;
        }

    }
}
