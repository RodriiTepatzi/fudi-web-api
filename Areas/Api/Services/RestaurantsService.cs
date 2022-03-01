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
            DocumentReference documentReference = resRef.AddAsync(data).GetAwaiter().GetResult();

            return data;
        }

        public List<Restaurant> RestaurantsByCategory(List<string> category)
        {
            Query query = _fireStoreDb.Collection(route).WhereIn(nameof(Restaurant.category), category);
            
            return QueryRecords(query);
        }
        
    }
}