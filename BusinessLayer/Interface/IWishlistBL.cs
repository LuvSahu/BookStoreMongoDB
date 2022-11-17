using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishlistBL
    {
        public WishlistModel AddToWishlist(WishlistModel wish,string userid);

        public bool RemoveWishlist(string id);

        public IEnumerable<WishlistModel> GetWishlist();

    }
}
