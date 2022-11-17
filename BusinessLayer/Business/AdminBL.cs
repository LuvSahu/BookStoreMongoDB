using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public AdminRegisterModel Register(AdminRegisterModel register)
        {
            try
            {
                return adminRL.Register(register);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Login(AdminLoginModel login)
        {
            try
            {
                return adminRL.Login(login);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
