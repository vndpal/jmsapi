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
    public class FillingDepartment : IFillingDepartment
    {
        private readonly IDbConnections _conn;
        public FillingDepartment(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddFilling(FillingDepartmentDto filling)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                var procResult = _conn.ExecuteProcedure("InsertUpdateFilling", new SqlParameter("ProcType", "INSERT")
                                                                              , new SqlParameter("FillingId", 0)
                                                                              , new SqlParameter("JobId", filling.JobId)
                                                                              , new SqlParameter("IssuedDate", filling.IssuedDate)
                                                                              , new SqlParameter("ReceivedDate", filling.ReceivedDate)
                                                                              , new SqlParameter("IssuedWeight ", filling.IssuedWeight)
                                                                              , new SqlParameter("RawGhatWeight", filling.RawGhatWeight)
                                                                              , new SqlParameter("PercentageLoss", filling.PercentageLoss)
                                                                              , new SqlParameter("EmployeeId", filling.EmployeeId)
                                                                              , new SqlParameter("Loss", filling.Loss)
                                                                              , new SqlParameter("ExtraLoss", filling.ExtraLoss)
                                                                              , new SqlParameter("Status", filling.Status)
                                                                              , new SqlParameter("Remark", filling.Remark));

                if (procResult != null)
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

        public async Task<SingleReturnResult<string>> UpdateFilling(FillingDepartmentDto filling)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                var procResult = _conn.ExecuteProcedure("InsertUpdateFilling", new SqlParameter("ProcType", "UPDATE")
                                                                              , new SqlParameter("FillingId", filling.FillingId)
                                                                              , new SqlParameter("JobId", filling.JobId)
                                                                              , new SqlParameter("IssuedDate", filling.IssuedDate)
                                                                              , new SqlParameter("ReceivedDate", filling.ReceivedDate)
                                                                              , new SqlParameter("IssuedWeight ", filling.IssuedWeight)
                                                                              , new SqlParameter("RawGhatWeight", filling.RawGhatWeight)
                                                                              , new SqlParameter("PercentageLoss", filling.PercentageLoss)
                                                                              , new SqlParameter("EmployeeId", filling.EmployeeId)
                                                                              , new SqlParameter("Loss", filling.Loss)
                                                                              , new SqlParameter("ExtraLoss", filling.ExtraLoss)
                                                                              , new SqlParameter("Status", filling.Status)
                                                                              , new SqlParameter("Remark", filling.Remark));

                if (procResult != null)
                {
                    result.Flag = ApplicationConstants.successFlag;
                    result.message = "Data Updated Successfully";
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

        public async Task<ListReturnResult<FillingDepartmentDto>> GetFillingJobs()
        {
            ListReturnResult<FillingDepartmentDto> filling = new ListReturnResult<FillingDepartmentDto>();
            try
            {
                string SqlQuery = "GetFilling";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    filling.result = connection.Query<FillingDepartmentDto>(SqlQuery ,commandType:CommandType.StoredProcedure).AsList();

                }
                filling.Flag = ApplicationConstants.successFlag;
                filling.message = "Data Fetched Successfully";
                return filling;


            }
            catch (Exception ex)
            {
                filling.Flag = ApplicationConstants.failureFlag;
                filling.message = ex.ToString();
                return filling;
            }
        }

        public async Task<SingleReturnResult<FillingDepartmentDto>> GetFillingJobWithId(int Id)
        {
            SingleReturnResult<FillingDepartmentDto> filling = new SingleReturnResult<FillingDepartmentDto>();
            try
            {
                string SqlQuery = "GetFilling";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    filling.result = await connection.QueryFirstOrDefaultAsync<FillingDepartmentDto>(SqlQuery, new { FillingId = Id },commandType:CommandType.StoredProcedure);
                }
                filling.Flag = ApplicationConstants.successFlag;
                filling.message = "Data Fetched Successfully";
                return filling;

            }
            catch (Exception ex)
            {
                filling.Flag = ApplicationConstants.failureFlag;
                filling.message = ex.ToString();
                return filling;
            }
        }

        public async Task<ListReturnResult<AssignedJobDTO>> GetFillingAssignedJob()
        {
            ListReturnResult<AssignedJobDTO> filling = new ListReturnResult<AssignedJobDTO>();
            try
            {
                string SqlQuery = "GetAssignedJob";
                var values = new { StatusId = 6 };
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    filling.result = connection.Query<AssignedJobDTO>(SqlQuery, values, commandType: CommandType.StoredProcedure).AsList();
                }
                filling.Flag = ApplicationConstants.successFlag;
                filling.message = "Data Fetched successfully";
                return filling;
            }
            catch (Exception ex)
            {
                filling.Flag = ApplicationConstants.failureFlag;
                filling.message = ex.ToString();
                return filling;
            }
        }

        public async Task<ListReturnResult<FillingReportDto>> GetFillingReport(int jobId ,int employeeId ,string fromDate ,string toDate , int status)
        {
            ListReturnResult<FillingReportDto> filling = new ListReturnResult<FillingReportDto>();
            try
            {
                string SqlQuery = "GetFillingReport";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    filling.result = connection.Query<FillingReportDto>(SqlQuery, new { JobId = jobId,EmployeeId = employeeId,FromDate = fromDate,ToDate=toDate,Status= status }, commandType: CommandType.StoredProcedure).AsList();
                }

                if (filling.result != null)
                {
                    filling.Flag = ApplicationConstants.successFlag;
                    filling.message = "Data Fetched Successfully!";
                }
                else
                {
                    filling.Flag = ApplicationConstants.failureFlag;
                    filling.message = "No Records found !";
                }
                return filling;
            }
            catch (Exception ex)
            {
                filling.Flag = ApplicationConstants.failureFlag;
                filling.message = ex.ToString();
                return filling;
            }
        }
    }
}
