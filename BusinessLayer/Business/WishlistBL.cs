using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class WishlistBL : IWishlistBL
    {
        private readonly IWishlistRL wishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }
        public WishlistModel AddToWishlist(WishlistModel wish,string userid)
        {
            try
            {
                return this.wishlistRL.AddToWishlist(wish,userid);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool RemoveWishlist(string id)
        {
            try
            {
                return this.wishlistRL.RemoveWishlist(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WishlistModel> GetWishlist()
        {
            try
            {
                return this.wishlistRL.GetWishlist();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

