using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IPolishDepartment
    {
        Task<SingleReturnResult<string>> AddPolish(PolishDepartmentDto polish);
        Task<SingleReturnResult<string>> UpdatePolish(PolishDepartmentDto polish);
        Task<ListReturnResult<PolishDepartmentDto>> GetPolishJobs();
        Task<SingleReturnResult<PolishDepartmentDto>> GetPolishJobWithId(int Id);
        Task<ListReturnResult<AssignedJobDTO>> GetPolishAssignedJob();
        Task<ListReturnResult<PolishReportDto>> GetPolishReport(int jobId, int employeeId, string fromDate, string toDate, int status);
    }
}
