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
        Task<ListReturnResult<JobMasterDto>> getJobMaster();
        Task<ListReturnResult<TrackJobDto>> TrackJob(string department, int jobId);
        Task<ListReturnResult<EmployeeDto>> GetEmployeeFromDepartment(int departmentId);
    }
}
