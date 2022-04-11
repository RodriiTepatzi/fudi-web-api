using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models
{
    public class Restaurant
    {
        [FirestoreProperty]
        public string uid { get; set; }

        [FirestoreProperty]
        public string restaurantName { get; set; }

        [FirestoreProperty]
        public string restaurantAddress { get; set; }

        [FirestoreProperty]
        public string restaurantUrl { get; set; }

        [FirestoreProperty]
        public string restaurantSlogan { get; set; }

        [FirestoreProperty]
        public int stars { get; set; }

        [FirestoreProperty]
        public string cost { get; set; }

        [FirestoreProperty]
        public string category { get; set; }

        [FirestoreProperty]
        public string status { get; set; }

        [FirestoreProperty]
        public DateTime startDate { get; set; }

        public List<Product> products { get; set; }

        public Dictionary<string, object> ToMap()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("uid", this.uid);
            data.Add("restaurantName", this.restaurantName);
            data.Add("restaurantAddress", this.restaurantAddress);
            data.Add("restaurantSlogan", this.restaurantSlogan);
            data.Add("restaurantUrl", this.restaurantUrl);
            data.Add("stars", this.stars);
            data.Add("cost", this.cost);
            data.Add("status", this.status);
            data.Add("category", this.category);
            data.Add("startDate", Timestamp.FromDateTime(DateTime.SpecifyKind(this.startDate, DateTimeKind.Utc)));

            return data;
        }
    }
}
