using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UsersController(IUserBL userBL)
        {
            this.userBL = userBL;

        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel register)
        {
            try
            {

                var result = userBL.Register(register);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Regestration Successfull", data = result });
                }
                else
                {

                    return BadRequest(new { success = false, message = "Regestration not Successfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        [HttpPost("Login")]

        public ActionResult Login(LoginModel login)
        {
            try
            {
                var result = userBL.Login(login);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login is  Succecsfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login is not Successfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //[Authorize]
        [HttpPost("ForgotPassword")]
        public ActionResult FogotPassword(string Email)
        {
            try
            {
                var result = userBL.FogotPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset link sent Succecsfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset link sending failed" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("ResetLink")]
        public ActionResult ResetLink(string password, string confirmPassword)
        {

            try
            {

                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();

                var result = userBL.ResetLink(Email, password, confirmPassword);

                if (result != null)
                {
                    return Ok(new { success = true, message = "REST LINK SEND SUCCESSFULL" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "REST LINK SEND FAILED" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
