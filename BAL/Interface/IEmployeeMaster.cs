using DTO.DTOModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IEmployeeMaster
    {
        Task<ListReturnResult<EmployeeDto>> GetAllEmployee();
        Task<SingleReturnResult<EmployeeDto>> GetEmployee(int Id);
        Task<SingleReturnResult<FilesDto>> getFile(string group, int refId, string type);
        Task<SingleReturnResult<string>> AddUpdateEmployee(EmployeeDto employeeDto, IFormFileCollection Files);
    } 
}
