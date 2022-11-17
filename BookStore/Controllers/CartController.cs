using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost]
        [Route("Addtocart")]
        public IActionResult AddtoCart(CartModel cart)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var check = this.cartBL.AddtoCart(cart,userId);
                if (check != null)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Book Added to Cart", Data = check });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book Not Added to Cart" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpPut]
        [Route("Updatequantity")]
        public IActionResult UpdateCartQty(CartModel edit, string id)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var check = this.cartBL.UpdateCartQty(edit,id,userId);
                if (check != null)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Book Quantity Updated", Data = check });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Cannot Update Quantity" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpDelete]
        [Route("Deletefromcart")]
        public IActionResult RemovefromCart(string id)
        {
            try
            {
                var check = this.cartBL.RemovefromCart(id);
                if (check != false)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Book Removed from Cart" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Removed from Cart" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("Getcart")]
        public IActionResult GetCart()
        {
            try
            {
                IEnumerable<CartModel> check = this.cartBL.GetCart();
                if (check != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart Retrived Successfully", Data = check });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Cart is Empty" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }

}
