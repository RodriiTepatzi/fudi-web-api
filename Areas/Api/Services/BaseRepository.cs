using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace fudi_web_api.Areas.Api.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        string filepath = "C:/Users/sprno/source/repos/fudi-web-api/fudiapp-firebase-adminsdk-8gwp6-561920ca6d.json";
        public FirestoreDb _fireStoreDb;
        string collection;

        public BaseRepository(string collectionName)
        {
            this.collection = collectionName;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            _fireStoreDb = FirestoreDb.Create("fudiapp");
        }
        public T Get(string id)
        {
            DocumentReference docRef = _fireStoreDb.Collection(collection).WhereEqualTo("uid", id);
            DocumentSnapshot snapshot = docRef.GetSnapshotAsync().GetAwaiter().GetResult();
            if (snapshot.Exists)
            {
                T usr = snapshot.ConvertTo<T>();
                //usr.Id = snapshot.Id;
                return usr;
            }
            else
            {
                return null;
            }
        }

        public List<T> GetAll()
        {
            Query query = _fireStoreDb.Collection(collection);
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<T> list = new List<T>();

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists) {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(data);
                    T newItem = JsonConvert.DeserializeObject<T>(json);
                    list.Add(newItem);
                }
            }

            return list;
        }

        public T Add(T record)
        {
            CollectionReference colRef = _fireStoreDb.Collection(collection);
            DocumentReference doc = colRef.AddAsync(record).GetAwaiter().GetResult();
            //record.Id = doc.Id;
            return record;
        }

        public bool Update(T record)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T record)
        {
            throw new NotImplementedException();
        }
    }
}
