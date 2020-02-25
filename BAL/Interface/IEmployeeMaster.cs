using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IEmployeeMaster
    {
        Task<SingleReturnResult<string>> AddUpdateEmployee(List<EmployeeDto> employeeDto);
        Task<ListReturnResult<EmployeeDto>> GetAllCompany();
    } 
}
