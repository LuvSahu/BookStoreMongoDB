using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        public AdminRegisterModel Register(AdminRegisterModel register);

        public string Login(AdminLoginModel login);
    }
}
