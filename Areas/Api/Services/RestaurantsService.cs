using fudi_web_api.Models;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Areas.Api.Services
{
    public class RestaurantsService : BaseRepository<Restaurant>
    {
        private string route;
        public RestaurantsService(string route) : base(route)
        {
            this.route = route;
        }

        public Restaurant AddRestaurant(Restaurant data)
        {
            CollectionReference resRef = _fireStoreDb.Collection("restaurants");
            //DocumentReference documentReference = resRef.AddAsync(data.ToMap()).GetAwaiter().GetResult();
            DocumentReference documentReference = resRef.Document();
            data.uid = documentReference.Id;
            documentReference.SetAsync(data.ToMap());

            return data;
        }

        public List<Restaurant> RestaurantsByCategory(List<string> category)
        {
            Query query = _fireStoreDb.Collection(route).WhereIn(nameof(Restaurant.category), category);
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<Restaurant> list = new List<Restaurant>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    data["startDate"] = DateTime.ParseExact(data["startDate"].ToString().Replace("Timestamp:", "").Trim(),
                        "yyyy-MM-ddTHH:mm:ssZ", null);
                    string json = JsonConvert.SerializeObject(data);
                    Restaurant newItem = JsonConvert.DeserializeObject<Restaurant>(json);
                    list.Add(newItem);
                }
            }
            return list;
        }

        public List<Restaurant> GetAllRestaurants()
        {
            Query query = _fireStoreDb.Collection("restaurants");
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<Restaurant> list = new List<Restaurant>();

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    data["startDate"] = DateTime.ParseExact(data["startDate"].ToString().Replace("Timestamp:", "").Trim(),
                        "yyyy-MM-ddTHH:mm:ssZ", null);
                    string json = JsonConvert.SerializeObject(data);
                    Restaurant newItem = JsonConvert.DeserializeObject<Restaurant>(json);
                    list.Add(newItem);
                }
            }

            return list;
        }
        
    }
}