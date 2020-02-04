using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IJobMaster
    {
        Task<SingleReturnResult<string>> AddUpdateJob(JobMasterDto job);
        Task<ListReturnResult<JobMasterDto>> GetAllJob();
        Task<SingleReturnResult<JobMasterDto>> GetJob(int Id);
       // Task<SingleReturnResult<JobMasterDto>> updateJob(JobMasterDto jobDetails);
    }
}
