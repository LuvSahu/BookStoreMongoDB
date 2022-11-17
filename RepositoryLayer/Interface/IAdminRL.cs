using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAdminRL
    {
        public AdminRegisterModel Register(AdminRegisterModel register);

        public string Login(AdminLoginModel login);
    }
}
