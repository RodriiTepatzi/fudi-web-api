using fudi_web_api.Models;
using fudi_web_api.Models.Cart;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fudi_web_api.Areas.Api.Services
{
    public class CartService : BaseRepository<Cart>
    {
        private string route;
        private ProductsService _productsService = new ProductsService("products");
        public CartService(string route) : base (route)
        {
            this.route = route;
        }

        public List<Cart> GetCart(string id)
        {
            CollectionReference usersCollection =  _fireStoreDb.Collection(route);
            DocumentReference userDocument = usersCollection.Document(id);
            CollectionReference cartCollection = userDocument.Collection("cart");
            QuerySnapshot result = cartCollection.GetSnapshotAsync().GetAwaiter().GetResult();
            List<Cart> carts = new List<Cart>();

            foreach (var documentSnapshot in result)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    data["closedDate"] = DateTime.ParseExact(data["closedDate"].ToString().Replace("Timestamp:", "").Trim(),
                        "yyyy-MM-ddTHH:mm:ssZ", null);
                    data["openedDate"] = DateTime.ParseExact(data["openedDate"].ToString().Replace("Timestamp:", "").Trim(),
                        "yyyy-MM-ddTHH:mm:ssZ", null);
                    string json = JsonConvert.SerializeObject(data);
                    Cart newItem = JsonConvert.DeserializeObject<Cart>(json);
                    
                    carts.Add(newItem);
                }
            }

            foreach (var cart in carts)
            {
                DocumentReference orderReference = cartCollection.Document(cart.uid);
                CollectionReference ordersCollection = orderReference.Collection("orders");
                cart.orders = new List<Models.Cart.Order>();
                foreach (var item in ordersCollection.GetSnapshotAsync().GetAwaiter().GetResult())
                {
                    if (item.Exists)
                    {
                        Dictionary<string, object> orderData = item.ToDictionary();
                        string jsonOrderData = JsonConvert.SerializeObject(orderData);
                        Models.Cart.Order order = JsonConvert.DeserializeObject<Models.Cart.Order>(jsonOrderData);
                        cart.orders.Add(order);
                    }
                }

                foreach (var orderDataFromList in cart.orders)
                {
                    orderDataFromList.orderItems = new List<OrderItem>();
                    DocumentReference orderItemReference = ordersCollection.Document(orderDataFromList.id);
                    CollectionReference orderItemsCollectionReference = orderItemReference.Collection("orderItems");

                    // Here we create orderItems 
                    foreach (var item in orderItemsCollectionReference.GetSnapshotAsync().GetAwaiter().GetResult())
                    {
                        if (item.Exists)
                        {
                            Dictionary<string, object> orderItemData = item.ToDictionary();
                            string jsonOrderItemData = JsonConvert.SerializeObject(orderItemData);
                            OrderItem order = JsonConvert.DeserializeObject<OrderItem>(jsonOrderItemData);
                            orderDataFromList.orderItems.Add(order);
                        }
                    }
                    

                    foreach (var orderItem in orderDataFromList.orderItems)
                    {
                        orderItem.restaurant = null;
                        orderItem.products = new List<OrderProduct>();

                        DocumentReference orderItemProductsReference = orderItemsCollectionReference.Document(orderItem.id);
                        CollectionReference productsReference = orderItemProductsReference.Collection("products");
                        CollectionReference restaurantReference = orderItemProductsReference.Collection("restaurant");

                        foreach (var item in restaurantReference.GetSnapshotAsync().GetAwaiter().GetResult())
                        {
                            if (item.Exists)
                            {
                                Dictionary<string, object> restaurantData = item.ToDictionary();
                                restaurantData["startDate"] = DateTime.ParseExact(restaurantData["startDate"].ToString().Replace("Timestamp:", "").Trim(),
                                    "yyyy-MM-ddTHH:mm:ssZ", null);
                                string jsonRestaurantData = JsonConvert.SerializeObject(restaurantData);
                                Restaurant restaurant = JsonConvert.DeserializeObject<Restaurant>(jsonRestaurantData);
                                orderItem.restaurant = restaurant;
                            }
                        }

                        foreach (var item in productsReference.GetSnapshotAsync().GetAwaiter().GetResult())
                        {
                            if (item.Exists)
                            {
                                Dictionary<string, object> productData = item.ToDictionary();
                                string jsonProductData = JsonConvert.SerializeObject(productData);
                                OrderProduct orderProduct = JsonConvert.DeserializeObject<OrderProduct>(jsonProductData);
                                DocumentReference productDocument = productsReference.Document(orderProduct.id);
                                CollectionReference productCollection = productDocument.Collection("product");
                                foreach (var productDataSnapshot in productCollection.GetSnapshotAsync().GetAwaiter().GetResult())
                                {
                                    if (productDataSnapshot.Exists)
                                    {
                                        Dictionary<string, object> productLastData = productDataSnapshot.ToDictionary();
                                        string jsonProductLastData = JsonConvert.SerializeObject(productLastData);
                                        Product product = JsonConvert.DeserializeObject<Product>(jsonProductLastData);
                                        orderProduct.product = product;
                                    }
                                }
                                orderItem.products.Add(orderProduct);
                            }
                        }
                    }
                }
            }

            return carts;
        }

        public Cart UpdateCart(string id, Cart cart)
        {
            CollectionReference usersCollection = _fireStoreDb.Collection(route);
            DocumentReference userDocument = usersCollection.Document(id);
            CollectionReference cartCollection = userDocument.Collection("cart");
            DocumentReference cartReference = cartCollection.Document(cart.uid);
            cartReference.UpdateAsync(cart.ToMap()).GetAwaiter();
            foreach (var order in cart.orders)
            {
                CollectionReference cartOrdersCollectionReference = cartReference.Collection("orders");
                DocumentReference orderDocumentReference = cartOrdersCollectionReference.Document(order.id);
                orderDocumentReference.SetAsync(order.ToMap()).GetAwaiter();
                foreach (var orderItem in order.orderItems)
                {
                    CollectionReference cartOrderItemsCollectionReference = orderDocumentReference.Collection("orderItems");
                    DocumentReference orderItemDocumentReference = cartOrderItemsCollectionReference.Document(order.id);
                    orderItemDocumentReference.SetAsync(orderItem.ToMap()).GetAwaiter();
                    CollectionReference productsCollectionReference = orderItemDocumentReference.Collection("products");
                    CollectionReference restaurantCollectionReference = orderItemDocumentReference.Collection("restaurant");
                    restaurantCollectionReference.Document(orderItem.restaurant.uid).SetAsync(orderItem.restaurant.ToMap()).GetAwaiter();
                    foreach (var orderProduct in orderItem.products) 
                    {
                        DocumentReference orderProductReference = productsCollectionReference.Document(orderProduct.id);
                        orderProductReference.UpdateAsync(orderProduct.ToMap()).GetAwaiter();
                        CollectionReference finalProductCollection = orderProductReference.Collection("product");
                        finalProductCollection.Document(orderProduct.product.productId).SetAsync(orderProduct.product.ToMap()).GetAwaiter();
                    }
                }
            }

            return cart;
        }
    }
}