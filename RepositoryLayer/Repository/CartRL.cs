using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Repository
{
    public class CartRL : ICartRL
    {
        private readonly IMongoCollection<CartModel> Cart;

        public CartRL(IDBSetting db)
        {
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Cart = database.GetCollection<CartModel>("Cart");
        }
        public CartModel AddtoCart(CartModel cart,string userid)
        {
            try
            {
                var check = this.Cart.Find(x => x.cartID == cart.cartID && x.userID == userid).SingleOrDefault();
                if (check == null)
                {
                    this.Cart.InsertOne(cart);
                    return cart;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool RemovefromCart(string id)
        {
            try
            {
                this.Cart.FindOneAndDelete(x => x.cartID == id);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public CartModel UpdateCartQty(CartModel qty, string id,string userid)
        {
            try
            {
                var check = this.Cart.Find(x => x.cartID == id && x.userID == userid).FirstOrDefault();
                if (check != null)
                {
                    this.Cart.UpdateOne(x => x.cartID == id,Builders<CartModel>.Update.Set(x => x.quantity, qty.quantity));
                    return check;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CartModel> GetCart()
        {
            return Cart.Find(FilterDefinition<CartModel>.Empty).ToList();

        }
    }
}

