using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models
{
    public class Product
    {
        [FirestoreProperty]
        public string productId { get; set; }

        [FirestoreProperty]
        public string productName { get; set; }

        [FirestoreProperty]
        public string productDescription { get; set; }

        [FirestoreProperty]
        public double productPrice { get; set; }

        [FirestoreProperty]
        public string productUrl { get; set; }

        [FirestoreProperty]
        public int productLikes { get; set; }

        [FirestoreProperty]
        public string productUnit { get; set; } 

        public Dictionary<string, object> ToMap()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("productId", this.productId);
            data.Add("productName", this.productName);
            data.Add("productDescription", this.productDescription);
            data.Add("productPrice", this.productPrice);
            data.Add("productUrl", this.productUrl);
            data.Add("productLikes", this.productLikes);
            data.Add("productUnit", this.productUnit);

            return data;
        }
    }
}
