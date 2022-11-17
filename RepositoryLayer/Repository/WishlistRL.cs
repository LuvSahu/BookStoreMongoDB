using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Repository
{
    public class WishlistRL : IWishlistRL
    {
        private readonly IMongoCollection<WishlistModel> Wishlist;

        public WishlistRL(IDBSetting db)
        {
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Wishlist = database.GetCollection<WishlistModel>("Wishlist");
        }
        public WishlistModel AddToWishlist(WishlistModel wish,string userid)
        {
            try
            {
                var check = this.Wishlist.Find(x => x.wishlistID == wish.wishlistID && x.userID == userid).SingleOrDefault();
                if (check == null)
                {
                    this.Wishlist.InsertOne(wish);
                    return wish;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool RemoveWishlist(string id)
        {
            try
            {
                this.Wishlist.FindOneAndDelete(x => x.wishlistID == id);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WishlistModel> GetWishlist()
        {
            return Wishlist.Find(FilterDefinition<WishlistModel>.Empty).ToList();
        }
    }
}

