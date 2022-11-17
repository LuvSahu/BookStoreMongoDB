using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public RegisterModel Register(RegisterModel register);

        public string Login(LoginModel login);

        public string FogotPassword(string Email);

        public bool ResetLink(string Email, string password, string confirmPassword);

    }
}
