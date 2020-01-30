using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IJobMaster
    {
        Task<SingleReturnResult<string>> AddUpdateJob(List<JobMasterDto> comp);
        Task<ListReturnResult<JobMasterDto>> GetAllJob();
        Task<SingleReturnResult<JobMasterDto>> GetJob(int JobId);
       // Task<SingleReturnResult<JobMasterDto>> updateJob(JobMasterDto jobDetails);
    }
}
