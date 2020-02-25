using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICommonMaster
    {
        Task<ListReturnResult<CommonMasterDto>> getCommonDetailsAsync(string mstGroup);
    }
}
