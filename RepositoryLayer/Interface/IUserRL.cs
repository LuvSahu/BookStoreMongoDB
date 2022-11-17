using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public RegisterModel Register(RegisterModel register);

        public string Login(LoginModel login);
        public string FogotPassword(string Email);

        public bool ResetLink(string Email, string password, string confirmPassword);

    }
}
