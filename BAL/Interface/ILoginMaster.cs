using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ILoginMaster
    {
        Task<SingleReturnResult<string>> AuthenticateUser(LoginDto loginDetails);
        //Task<SingleReturnResult<LoginDto>> RegisterUser(LoginDto userDetails);
        // new asndlkasndlnasld
    }
}
