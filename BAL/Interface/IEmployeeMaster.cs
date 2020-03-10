using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IEmployeeMaster
    {
        Task<SingleReturnResult<string>> AddUpdateEmployee(EmployeeDto employeeDto);
        Task<ListReturnResult<EmployeeDto>> GetAllEmployee();
        Task<SingleReturnResult<EmployeeDto>> GetEmployee(int Id);
    } 
}
