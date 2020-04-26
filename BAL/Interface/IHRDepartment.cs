using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
     public interface IHRDepartment
    {
        Task<SingleReturnResult<string>> AddHR(HRDepartmentDto hr);
        Task<ListReturnResult<AssignedJobDTO>> GetHRAssignedJob();
        Task<ListReturnResult<HRDepartmentDto>> GetHRDepartmentJobs();
    }
}
