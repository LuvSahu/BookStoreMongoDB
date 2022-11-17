using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;

        }

        [HttpPost]
        [Route("AdminRegister")]
        public IActionResult Register(AdminRegisterModel register)
        {
            try
            {

                var result = adminBL.Register(register);
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

        [HttpPost("AdminLogin")]

        public ActionResult Login(AdminLoginModel login)
        {
            try
            {
                var result = adminBL.Login(login);
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
    }
}
