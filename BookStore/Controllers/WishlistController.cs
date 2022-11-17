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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL wishlistBL;

        public WishlistController(IWishlistBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        [HttpPost]
        [Route("Addtowishlist")]
        public IActionResult AddToWishlist(WishlistModel wish)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var check = this.wishlistBL.AddToWishlist(wish,userId);
                if (check != null)
                {
                    return this.Ok(new ResponseModel<WishlistModel> { Status = true, Message = "Book Added to Wishlist", Data = check });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book Not Added to Wishlist" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpDelete]
        [Route("Removefromwishlist")]
        public IActionResult RemoveWishlist(string id)
        {
            try
            {
                var check = this.wishlistBL.RemoveWishlist(id);
                if (check != false)
                {
                    return this.Ok(new ResponseModel<WishlistModel> { Status = true, Message = "Book Removed from Wishlist" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Removed from Wishlist" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("Getwishlist")]
        public IActionResult GetWishlist()
        {
            try
            {
                IEnumerable<WishlistModel> check = this.wishlistBL.GetWishlist();
                if (check != null)
                {
                    return this.Ok(new { Status = true, Message = "Wishlist Retrived Successfully", Data = check });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Wishlist is Empty" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
    