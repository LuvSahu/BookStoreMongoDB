using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }


        [Authorize]
        [HttpPost]
        [Route("Addorder")]
        public IActionResult AddOrder(OrderModel order)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var check = this.orderBL.AddOrder(order,userId);
                if (check != null)
                {
                    return this.Ok(new ResponseModel<OrderModel> { Status = true, Message = "Order Placed", Data = check });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order not Placed" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Cancleorder")]
        public IActionResult CancleOrder(string id)
        {
            try
            {
                var check = this.orderBL.CancleOrder(id);
                if (check != false)
                {
                    return this.Ok(new ResponseModel<OrderModel> { Status = true, Message = "Order Canclled" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order not Canclled" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Getorder")]
        public IActionResult GetOrder()
        {
            try
            {
                IEnumerable<OrderModel> check = this.orderBL.GetOrder();
                if (check != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Retrived Successfully", Data = check });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order is Empty" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}

