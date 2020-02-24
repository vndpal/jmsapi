using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IWAXDepartment
    {
        Task<SingleReturnResult<string>> AddWAX(List<WAXDepartmentDto> wax);
        Task<ListReturnResult<WAXDepartmentDto>> GetAllWAX();
        Task<SingleReturnResult<WAXDepartmentDto>> GetWAX(int Id);
    }
}
