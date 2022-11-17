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
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;

        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [HttpPost]
        [Route("Addaddress")]
        public IActionResult AddAddress(AddressModel add)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var check = this.addressBL.AddAddress(add,userId);
                if (check != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Added Successfully", Data = check });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Added" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }

        [HttpPut]
        [Route("Updateaddress")]
        public IActionResult UpdateAddress(AddressModel edit, string id)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var check = this.addressBL.UpdateAddress(edit,id,userId);
                if (check != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Updated Successfully", Data = check });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Updated" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }

        [HttpDelete]
        [Route("Deleteaddress")]
        public IActionResult DeleteAddress(string id)
        {
            try
            {
                var check = this.addressBL.DeleteAddress(id);
                if (check != false)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Deleted Successfully" });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Deleted" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("Getalladdress")]
        public IActionResult GetallAddress()
        {
            try
            {
                IEnumerable<AddressModel> check = this.addressBL.GetallAddress();
                if (check != null)
                {
                    return this.Ok(new { Status = true, Message = "Address Retrived Successfully", Data = check });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Retrived" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("getbyaddresstype")]
        public IActionResult GetByAddressType(string addtypeId)
        {
            try
            {
                var check = this.addressBL.GetByAddressType(addtypeId);
                if (check != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Retrived Successfully", Data = check });
                }
                else
                {
                    return this.Ok(new { Status = false, Message = "Address not Retrived" });
                }
            }
            catch (Exception e)
            {
                return this.Ok(new { Status = false, Message = e.Message });
            }
        }
    }
}


