﻿using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public CartModel AddtoCart(CartModel cart,string userid);

        public bool RemovefromCart(string id);

        public CartModel UpdateCartQty(CartModel qty, string id,string userid);

        public IEnumerable<CartModel> GetCart();




    }
}
