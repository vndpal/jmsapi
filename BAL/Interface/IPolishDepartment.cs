using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IPolishDepartment
    {
        Task<SingleReturnResult<string>> AddPolish(List<PolishDepartmentDto> polish);
        Task<ListReturnResult<PolishDepartmentDto>> GetAllPolish();
        Task<SingleReturnResult<PolishDepartmentDto>> GetPolish(int Id);
    }
}
