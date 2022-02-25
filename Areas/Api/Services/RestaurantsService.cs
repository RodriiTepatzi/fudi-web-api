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
        public RestaurantsService(string route) : base(route)
        {
            
        }

        public List<Product> GetProductsByRestaurantId (string uid)
        {
            List<Product> products = new List<Product>();
            CollectionReference prodRef = _fireStoreDb.Collection("restaurants/" + uid + "/products");
            QuerySnapshot snapshot = prodRef.GetSnapshotAsync().GetAwaiter().GetResult();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Dictionary<string, object> data = document.ToDictionary();
                string json = JsonConvert.SerializeObject(data);
                Product newItem = JsonConvert.DeserializeObject<Product>(json);
                products.Add(newItem);
            }

            return products;
        }

        public Restaurant AddRestaurant(Restaurant data)
        {
            CollectionReference resRef = _fireStoreDb.Collection("restaurants");
            DocumentReference documentReference = resRef.AddAsync(data).GetAwaiter().GetResult();

            return data;
        }

        
    }
}