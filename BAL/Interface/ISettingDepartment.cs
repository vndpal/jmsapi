using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ISettingDepartment
    {
        Task<SingleReturnResult<string>> AddSetting(List<SettingDepartmentDto> set);
        Task<ListReturnResult<SettingDepartmentDto>> GetAllSetting();
        SingleReturnResult<JobMasterDto> GetSettingAssignedJob();
    }
}
