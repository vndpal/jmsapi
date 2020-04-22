using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IFillingDepartment
    {
        Task<SingleReturnResult<string>> AddFilling(FillingDepartmentDto filling);
        Task<SingleReturnResult<string>> UpdateFilling(FillingDepartmentDto filling);
        Task<ListReturnResult<FillingDepartmentDto>> GetFillingJobs();
        Task<SingleReturnResult<FillingDepartmentDto>> GetFillingJobWithId(int Id);
        Task<ListReturnResult<AssignedJobDTO>> GetFillingAssignedJob();
    }
}
