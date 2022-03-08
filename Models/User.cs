using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string username { get; set; }

        [FirestoreProperty]
        public string uid { get; set; }

        [FirestoreProperty]
        public string telephone { get; set; }

        [FirestoreProperty]
        public string photoURL { get; set; }

        [FirestoreProperty]
        public string fullname { get; set; }

        [FirestoreProperty]
        public string email { get; set; }

        [FirestoreProperty]
        public DateTime birthday { get; set; }
    }
}
