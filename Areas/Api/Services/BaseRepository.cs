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
using System.Reflection;
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
        public async Task<string> Get(string id)
        {
            CollectionReference colRef = _fireStoreDb.Collection(collection);
            Query query = colRef.WhereEqualTo("uid", id);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                Dictionary<string, object> data = documentSnapshot.ToDictionary();
                string json = JsonConvert.SerializeObject(data);
                return json;
            }
            return null;
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

        public bool Update(string id, T values)
        {
            CollectionReference colRef = _fireStoreDb.Collection(collection);
            Query query = colRef.WhereEqualTo("uid", id);
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            string idDocument = "";

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                idDocument = documentSnapshot.Id;
            }

            DocumentReference recordRef = _fireStoreDb.Collection(collection).Document(idDocument);
            recordRef.SetAsync(values, SetOptions.Overwrite).GetAwaiter().GetResult();

            return true;
        }

        public bool Delete(string id)
        {
            CollectionReference colRef = _fireStoreDb.Collection(collection);
            Query query = colRef.WhereEqualTo("uid", id);
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            string idDocument = "";

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                idDocument = documentSnapshot.Id;
            }

            DocumentReference recordRef = _fireStoreDb.Collection(collection).Document(idDocument);
            recordRef.DeleteAsync().GetAwaiter().GetResult();

            return true;
        }
    }
}
