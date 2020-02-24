using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICASTDepartment
    {
        Task<SingleReturnResult<string>> AddCAST(List<CASTDepartmentDto> cast);
        Task<ListReturnResult<CASTDepartmentDto>> GetAllCAST();
        Task<SingleReturnResult<CASTDepartmentDto>> GetCAST(int Id);
    }
}
