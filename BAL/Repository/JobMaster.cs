using BLL.Interface;
using Dapper;
using DTO.DTOModels;
using Microsoft.AspNetCore.Http;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

        public async Task<SingleReturnResult<string>> AddUpdateJob(JobMasterDto job, IFormFileCollection files)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
               string NewJobNo = "";	
	                List<JobMasterDto> jobMaster = new List<JobMasterDto>();	
	                List<CompanyMasterDto> compMaster = new List<CompanyMasterDto>();
                DataTable dtJob = _conn.ToDataTable(job.diamondDetail);
                dtJob.Columns.Remove("DiaId");
                dtJob.Columns.Remove("JobId");
                dtJob.Columns.Remove("DiamondTypeValue");
                
                string SqlQuery = "SELECT TOP 1 * FROM JobMaster WHERE CompanyId =" + job.CompanyId + " ORDER BY JobNo DESC";	
	                using (var connection = new SqlConnection(_conn.strConnectionString()))	
	                {	
	                    await connection.OpenAsync();	
	                    jobMaster = connection.Query<JobMasterDto>(SqlQuery).AsList();	
	                }	
		
		
	                if (jobMaster.Count > 0)	
	                {	
	                    string[] jobno2 = jobMaster[0].JobNo.Split('-');	
	                    int id = Convert.ToInt32(jobno2[1]);	
	                    string idincrement = (id + 1).ToString();	
	                    NewJobNo = jobno2[0] + "-" + idincrement;	
	                }	
	                else	
	                {	
		
	                    string SqlQuery2 = "SELECT * FROM CompanyMaster WHERE CompanyId =" + job.CompanyId;	
	                    using (var connection = new SqlConnection(_conn.strConnectionString()))	
	                    {	
	                        await connection.OpenAsync();	
	                        compMaster = connection.Query<CompanyMasterDto>(SqlQuery2).AsList();	
	                    }	
	                    NewJobNo = compMaster[0].CompanyCode + "-1";	
	                }

                object stat = _conn.ExecuteProcedure("InsertUpdateJobMaster", new SqlParameter("JobId", job.JobId),
                                                                                    new SqlParameter("CompanyId", job.CompanyId),
                                                                                    new SqlParameter("JobNo", NewJobNo),
                                                                                    new SqlParameter("ClientJobNo", job.ClientJobNo),
                                                                                    new SqlParameter("JobType", job.JobType),
                                                                                    new SqlParameter("IssuedDate", job.IssuedDate),
                                                                                    new SqlParameter("DeliveryDate", job.DeliveryDate),
                                                                                    new SqlParameter("DesignImage", "jobimage"),
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

                    string operationType = "Update";
                    if (job.JobId == 0)
                    {
                        operationType = "Insert";
                    }
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                var fileBytes = ms.ToArray();

                                object sstat = _conn.ExecuteProcedure("UploadFileIntoDB", new SqlParameter("type", operationType)
                                                                                        , new SqlParameter("filedata", fileBytes)
                                                                                        , new SqlParameter("filetype", file.Name)
                                                                                        , new SqlParameter("filegroup", "Job")
                                                                                        , new SqlParameter("refId", int.Parse(stat.ToString()))
                                                                                        , new SqlParameter("filename", file.FileName)
                                                                                        , new SqlParameter("contentType", file.ContentType)
                                                                                        , new SqlParameter("uploadedBy", 1));

                            }
                        }
                    }
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
                string SqlQuery = "GetJobById";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    job.result = connection.Query<JobMasterDto>(SqlQuery , new { JobId = 0},commandType:CommandType.StoredProcedure).AsList();
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
                DataSet dsJob = await _conn.ExecuteProcedureForDataSet("GetJobById",new SqlParameter("JobId",Id));
                List<JobMasterDto> jobDetail = new List<JobMasterDto>();
                
                jobDetail = _conn.ConvertDataTable<JobMasterDto>(dsJob.Tables[0]);
                if (dsJob.Tables.Count > 1)
                {
                    jobDetail[0].diamondDetail = new List<DiamondDetailDto>();
                    jobDetail[0].diamondDetail = _conn.ConvertDataTable<DiamondDetailDto>(dsJob.Tables[1]); 
                }
              
                   job.result = jobDetail[0];
                    job.Flag = ApplicationConstants.successFlag;
                    job.message = "Data Fetched Successfully!"; 
                
                return job;
            }
            catch (Exception ex)
            {
                job.Flag = ApplicationConstants.failureFlag;
                job.message = ex.ToString();
                return job;
            }
        }

        //public async Task<ListReturnResult<TrackJobDto>> TrackJob(int JobId)
        //{
        //    ListReturnResult<TrackJobDto> Jobhistory = new ListReturnResult<TrackJobDto>();
        //    try
        //    {
        //        DataTable dsJob = await _conn.ExecuteProcedureForDataTable("TrackJob", new SqlParameter("JobId", JobId));
        //        List<TrackJobDto> jobDetail = new List<TrackJobDto>();

        //        jobDetail =  _conn.ConvertDataTable<TrackJobDto>(dsJob);

        //        TrackJobDto td = new TrackJobDto();
        //        td.crtDate = DateTime.Now;
        //        td.Department = "Test Department";
        //        td.EmployeeCode = "EMP-01";
        //        td.EmployeeId = 1;
        //        td.EmployeeName = "test test test";
        //        td.IssueDate = DateTime.Now;
        //        td.IssueWeight = 10;
        //        td.JobId = 1;
        //        td.StatusId = 10;
        //        jobDetail.Add(td);

        //        Jobhistory.result = jobDetail;
        //        Jobhistory.Flag = ApplicationConstants.successFlag;
        //        Jobhistory.message = "Data Fetched Successfully!";

        //        return Jobhistory;
        //    }
        //    catch (Exception ex)
        //    {
        //        Jobhistory.Flag = ApplicationConstants.failureFlag;
        //        Jobhistory.message = ex.ToString();
        //        return Jobhistory;
        //    }
        //}
    }
}
