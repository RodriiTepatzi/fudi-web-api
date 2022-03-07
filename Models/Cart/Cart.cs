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
        public List<Order> orders { get; set; }

        [FirestoreProperty]
        public string total { get; set; }

        [FirestoreProperty]
        public string openedDate { get; set; }

        [FirestoreProperty]
        public string closedDate { get; set; }
    }
}
