using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
     public interface IHRDepartment
    {
        Task<ListReturnResult<JobMasterDto>> GetHRAssignedJob();
        Task<ListReturnResult<HRDepartmentDto>> GetHRDepartmentJobs();
        Task<SingleReturnResult<string>> AddUpdateHR(List<HRDepartmentDto> hr);
    }
}
