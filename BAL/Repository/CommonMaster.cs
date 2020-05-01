using BLL.Interface;
using Dapper;
using DTO.DTOModels;
using Services.Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class CommonMaster : ICommonMaster
    {
        private readonly IDbConnections _conn;

        public CommonMaster(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<ListReturnResult<CommonMasterDto>> getCommonDetailsAsync(string mstGroup)
        {
            ListReturnResult<CommonMasterDto> commonResult = new ListReturnResult<CommonMasterDto>();
            try
            {
                string sqlQuery = "select * from commonMaster where MasterGroup=@MasterGroup and status=1";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    commonResult.result = connection.Query<CommonMasterDto>(sqlQuery, new { MasterGroup = mstGroup }).AsList();
                }

                commonResult.Flag = ApplicationConstants.successFlag;
                commonResult.message = "Data fetched Successfully from common masr !";
            }
            catch (Exception ex)
            {
                commonResult.Flag = ApplicationConstants.failureFlag;
                commonResult.message = "Some error has occured while fetching the data" + ex.ToString();
            }
            return commonResult;
        }
        
        public async Task<ListReturnResult<JobMasterDto>> getJobMaster()
        {
            ListReturnResult<JobMasterDto> jobData = new ListReturnResult<JobMasterDto>();
            try
            {
                string sqlQuery = "select JobId , JobNo ,ClientJobNo from JobMaster WHERE ProcessStatus = 0";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    jobData.result = connection.Query<JobMasterDto>(sqlQuery).AsList();
                }

                jobData.Flag = ApplicationConstants.successFlag;
                jobData.message = "Data fetched Successfully !";
            }
            catch (Exception ex)
            {
                jobData.Flag = ApplicationConstants.failureFlag;
                jobData.message = "Some error has occured while fetching the data" + ex.ToString();
            }
            return jobData;
        }

        public async Task<ListReturnResult<TrackJobDto>> TrackJob(string department , int jobId)
        {
            ListReturnResult<TrackJobDto> trackJob = new ListReturnResult<TrackJobDto>();
            try
            {
                string sqlQuery = "GetJobHistory";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    trackJob.result = connection.Query<TrackJobDto>(sqlQuery , new { Department = department , JobId = jobId},commandType:CommandType.StoredProcedure).AsList();
                }

                trackJob.Flag = ApplicationConstants.successFlag;
                trackJob.message = "Data fetched Successfully !";
            }
            catch (Exception ex)
            {
                trackJob.Flag = ApplicationConstants.failureFlag;
                trackJob.message = "Some error has occured while fetching the data" + ex.ToString();
            }
            return trackJob;
        }
    }
}
