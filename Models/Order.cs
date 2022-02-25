using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models
{
    public class Order
    {
        [FirestoreProperty]
        public string orderId { get; set; }

        [FirestoreProperty]
        public string userId { get; set; }

        [FirestoreProperty]
        public List<string> restaurantId { get; set; }

        [FirestoreProperty]
        public List<Product> products { get; set; }

        [FirestoreProperty]
        public string deliverId { get; set; }

        [FirestoreProperty]
        public string total { get; set; }

        [FirestoreProperty]
        public string IVA { get; set; }

        [FirestoreProperty]
        public string dateOpened { get; set; }

        [FirestoreProperty]
        public string dateClosed { get; set; }

        [FirestoreProperty]
        public string orderStatus { get; set; }
    }
}

