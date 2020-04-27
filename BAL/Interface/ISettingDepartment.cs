using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ISettingDepartment
    {
        Task<SingleReturnResult<string>> AddSetting(SettingDepartmentDto set);
        Task<SingleReturnResult<string>> UpdateSetting(SettingDepartmentDto set);
        Task<ListReturnResult<SettingDepartmentDto>> GetSettingJobs();
        SingleReturnResult<SettingDepartmentDto> GetSettingJobWithId(int Id);
        Task<ListReturnResult<AssignedJobDTO>> GetSettingAssignedJob();
        Task<ListReturnResult<DiamondDetailDto>> GetStoneForFitter(int Id);
    }
}
