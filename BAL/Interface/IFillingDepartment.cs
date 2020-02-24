using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IFillingDepartment
    {
        Task<SingleReturnResult<string>> AddFilling(List<FillingDepartmentDto> filling);
        Task<ListReturnResult<FillingDepartmentDto>> GetAllFilling();
        Task<SingleReturnResult<FillingDepartmentDto>> GetFilling(int Id);
    }
}
