using fudi_web_api.Models;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Areas.Api.Services
{
    public class ProductsService : BaseRepository<Product>
    {
        string route = "";
        public ProductsService(string route) : base(route)
        {
            this.route = route;    
        }

        public List<Product> GetAllProducts(string uid)
        {
            Query query = _fireStoreDb.Collection(route + "/" + uid + "/products");
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<Product> list = new List<Product>();

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(data);
                    Product newItem = JsonConvert.DeserializeObject<Product>(json);
                    newItem.requiredAddons = new List<Addon>();
                    newItem.optionalAddons = new List<Addon>();
                    CollectionReference requiredAddonsCollection = documentSnapshot.Reference.Collection("requiredAddons");
                    CollectionReference optionalAddonsCollection = documentSnapshot.Reference.Collection("optionalAddons");
                    foreach (var requiredAddonDocument in requiredAddonsCollection.GetSnapshotAsync().GetAwaiter().GetResult())
                    {
                        if (requiredAddonDocument.Exists)
                        {
                            Dictionary<string, object> addonDictionary = requiredAddonDocument.ToDictionary();
                            string addonJson = JsonConvert.SerializeObject(addonDictionary);
                            Addon addon = JsonConvert.DeserializeObject<Addon>(addonJson);
                            newItem.requiredAddons.Add(addon);
                        }
                    }

                    foreach (var optionalAddonDocument in optionalAddonsCollection.GetSnapshotAsync().GetAwaiter().GetResult())
                    { 
                        if (optionalAddonDocument.Exists)
                        {
                            Dictionary<string, object> addonDictionary = optionalAddonDocument.ToDictionary();
                            string addonJson = JsonConvert.SerializeObject(addonDictionary);
                            Addon addon = JsonConvert.DeserializeObject<Addon>(addonJson);
                            newItem.optionalAddons.Add(addon);
                        }
                    }
                    list.Add(newItem);
                }
            }

            return list;
        }

        public Product AddProductToRestaurant(string uid, Product product)
        {
            DocumentReference topDoc = _fireStoreDb.Collection("restaurants").Document(uid);
            CollectionReference subCollection = topDoc.Collection("products");
            DocumentReference document = subCollection.Document();
            product.productId = document.Id;
            document.CreateAsync(product.ToMap()).GetAwaiter();

            return product;
        }

        public Product UpdateProductInRestaurant(string uid, Product product)
        {
            DocumentReference topDoc = _fireStoreDb.Collection("restaurants").Document(uid);
            CollectionReference productsCollection = topDoc.Collection("products");
            DocumentReference productDocument = productsCollection.Document(product.productId);
            productDocument.SetAsync(product.ToMap()).GetAwaiter();
            return product;
        }

        public bool DeleteProductInrestaurant(string uid, string productId)
        {
            DocumentReference topDoc = _fireStoreDb.Collection("restaurants").Document(uid);
            CollectionReference productsCollection = topDoc.Collection("products");
            DocumentReference productDocument = productsCollection.Document(productId);
            productDocument.DeleteAsync().GetAwaiter();

            return true;
        }
    }
}
