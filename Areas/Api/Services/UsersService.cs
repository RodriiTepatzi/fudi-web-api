using fudi_web_api.Models;
using Google.Cloud.Firestore;
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
    }
}
