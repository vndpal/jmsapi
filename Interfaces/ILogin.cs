using DTO.LoginManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ILogin
    {
        LoginDto AuthenticateUser(LoginDto login);
    }
}
