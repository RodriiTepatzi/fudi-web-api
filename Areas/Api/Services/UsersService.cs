using fudi_web_api.Models;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Areas.Api.Services
{
    class UsersService : BaseRepository<User>
    {
        private string route;
        public UsersService(string route) : base(route)
        {
            this.route = route;
        }

        
        public User AddUser(User record)
        {
            CollectionReference colRef = _fireStoreDb.Collection(route);
            DocumentReference doc = colRef.Document(record.uid); 
            
            doc.SetAsync(record).GetAwaiter();
            
            return record;
        }

        public List<User> GetUserByNumber(List<string> number)
        {
            Query query = _fireStoreDb.Collection(route).WhereIn(nameof(User.telephone), number);
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<User> list = new List<User>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    data["birthday"] = DateTime.ParseExact(data["birthday"].ToString().Replace("Timestamp:", "").Trim(),
                        "yyyy-MM-ddTHH:mm:ssZ", null);
                    string json = JsonConvert.SerializeObject(data);
                    User newItem = JsonConvert.DeserializeObject<User>(json);
                    list.Add(newItem);
                }
            }
            return list;
        }
        public List<User> GetUserByUsername(List<string> username)
        {
            Query query = _fireStoreDb.Collection(route).WhereIn(nameof(User.username), username);
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<User> list = new List<User>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    data["birthday"] = DateTime.ParseExact(data["birthday"].ToString().Replace("Timestamp:", "").Trim(),
                        "yyyy-MM-ddTHH:mm:ssZ", null);
                    string json = JsonConvert.SerializeObject(data);
                    User newItem = JsonConvert.DeserializeObject<User>(json);
                    list.Add(newItem);
                }
            }
            return list;
        }
    }
}
