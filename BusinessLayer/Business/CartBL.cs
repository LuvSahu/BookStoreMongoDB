using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public CartModel AddtoCart(CartModel cart,string userid)
        {
            try
            {
                return this.cartRL.AddtoCart(cart,userid);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool RemovefromCart(string id)
        {
            try
            {
                return this.cartRL.RemovefromCart(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CartModel UpdateCartQty(CartModel qty, string id,string userid)
        {
            try
            {
                return this.cartRL.UpdateCartQty(qty, id,userid);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CartModel> GetCart()
        {
            try
            {
                return this.cartRL.GetCart();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
