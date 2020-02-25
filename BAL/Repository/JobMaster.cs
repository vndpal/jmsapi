using BLL.Interface;
using Dapper;
using DTO.DTOModels;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class JobMaster : IJobMaster
    {
        private readonly IDbConnections _conn;
        public JobMaster(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddUpdateJob(JobMasterDto job)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                DataTable dtJob = _conn.ToDataTable(job.diamondDetail);
                dtJob.Columns.Remove("DiaId");
                dtJob.Columns.Remove("JobId");

                object stat = _conn.ExecuteProcedure("InsertUpdateJobMaster", new SqlParameter("JobId", job.JobId),
                                                                                    new SqlParameter("CompanyId", job.CompanyId),
                                                                                    new SqlParameter("JobNo", job.JobNo),
                                                                                    new SqlParameter("ClientJobNo", job.ClientJobNo),
                                                                                    new SqlParameter("JobType", job.JobType),
                                                                                    new SqlParameter("IssuedDate", job.IssuedDate),
                                                                                    new SqlParameter("DeliveryDate", job.DeliveryDate),
                                                                                    new SqlParameter("WorkType", job.WorkType),
                                                                                    new SqlParameter("Remark", job.Remark),
                                                                                    new SqlParameter("ProcessStatus", job.ProcessStatus),
                                                                                    new SqlParameter("IsNewProcess", job.IsNewProcess),
                                                                                    new SqlParameter("DiamondDetail", dtJob));
                if (stat != null)
                {
                    result.Flag = ApplicationConstants.successFlag;
                    result.message = "Data Inserted Successfully";
                    result.result = "Ok";
                }
                else
                {
                    result.Flag = ApplicationConstants.failureFlag;
                    result.message = "some error has occured while inserting the data";
                    result.result = "";
                }

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                result.Flag = ApplicationConstants.failureFlag;
                result.message = ex.ToString();
                return result;
            }
        }

        public async Task<ListReturnResult<JobMasterDto>> GetAllJob()
        {
            ListReturnResult<JobMasterDto> job = new ListReturnResult<JobMasterDto>();
            try
            {
                string SqlQuery = "SELECT * FROM JobMaster";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    job.result = connection.Query<JobMasterDto>(SqlQuery).AsList();
                }
                job.Flag = ApplicationConstants.successFlag;
                job.message = "Data Fetched successfully";
                return job;
            }
            catch (Exception ex)
            {
                job.Flag = ApplicationConstants.failureFlag;
                job.message = ex.ToString();
                return job;
            }
        }

        public async Task<SingleReturnResult<JobMasterDto>> GetJob(int Id)
        {

            SingleReturnResult<JobMasterDto> job = new SingleReturnResult<JobMasterDto>();
            try
            {
                DataSet dsJob = _conn.ExecuteProcedureForDataSet("GetJobById",new SqlParameter("JobId",Id));
                List<JobMasterDto> jobDetail = new List<JobMasterDto>();
                jobDetail = _conn.ConvertDataTable<JobMasterDto>(dsJob.Tables[0]);
                jobDetail[0].diamondDetail = new List<DiamondDetailDto>();
                jobDetail[0].diamondDetail = _conn.ConvertDataTable<DiamondDetailDto>(dsJob.Tables[1]);
                //string SqlQuery = "SELECT * FROM JobMaster WHERE JobId = @JobId";

                //using (var connection = new SqlConnection(_conn.strConnectionString()))
                //{
                //    await connection.OpenAsync();
                //    job.result = await connection.QueryFirstOrDefaultAsync<JobMasterDto>(SqlQuery, new { JobId = Id  });
                //}
                if (job != null)
                {
                    job.result = jobDetail[0];
                    job.Flag = ApplicationConstants.successFlag;
                    job.message = "Data Fetched Successfully!"; 
                }
                return job;
            }
            catch (Exception ex)
            {
                job.Flag = ApplicationConstants.failureFlag;
                job.message = ex.ToString();
                return job;
            }
        }
    }
}
