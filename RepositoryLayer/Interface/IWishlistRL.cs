using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishlistRL
    {
        public WishlistModel AddToWishlist(WishlistModel wish,string userid);

        public bool RemoveWishlist(string id);

        public IEnumerable<WishlistModel> GetWishlist();



    }
}
