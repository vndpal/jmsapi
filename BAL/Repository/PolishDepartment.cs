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
    public class PolishDepartment : IPolishDepartment
    {
        private readonly IDbConnections _conn;
        public PolishDepartment(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddPolish(PolishDepartmentDto polish)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                var procResult = _conn.ExecuteProcedure("InsertUpdatePolish", new SqlParameter("ProcType", "INSERT")
                                                                              , new SqlParameter("PolishId", 0)
                                                                              , new SqlParameter("JobId", polish.JobId)
                                                                      
        , new SqlParameter("IssuedDate", polish.IssuedDate)
                                                                              , new SqlParameter("ReceivedDate", polish.ReceivedDate)
                                                                              , new SqlParameter("PolishType", polish.PolishType)
                                                                              , new SqlParameter("IssuedWeight ", polish.IssuedWeight)
                                                                              , new SqlParameter("ReceivedWeight", polish.ReceivedWeight)
                                                                              , new SqlParameter("WeightLoss", polish.WeightLoss)
                                                                              , new SqlParameter("Status", polish.Status)
                                                                              , new SqlParameter("Remark", polish.Remark));

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

        public async Task<SingleReturnResult<string>> UpdatePolish(PolishDepartmentDto polish)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                var procResult = _conn.ExecuteProcedure("InsertUpdatePolish", new SqlParameter("ProcType", "UPDATE")
                                                                              , new SqlParameter("PolishId", polish.PolishId)
                                                                              , new SqlParameter("JobId", polish.JobId)
                                                                              , new SqlParameter("IssuedDate", polish.IssuedDate)
                                                                              , new SqlParameter("ReceivedDate", polish.ReceivedDate)
                                                                              , new SqlParameter("PolishType", polish.PolishType)
                                                                              , new SqlParameter("IssuedWeight ", polish.IssuedWeight)
                                                                              , new SqlParameter("ReceivedWeight", polish.ReceivedWeight)
                                                                              , new SqlParameter("WeightLoss", polish.WeightLoss)
                                                                              , new SqlParameter("Status", polish.Status)
                                                                              , new SqlParameter("Remark", polish.Remark));

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

        public async Task<ListReturnResult<PolishDepartmentDto>> GetPolishJobs()
        {
            ListReturnResult<PolishDepartmentDto> polish = new ListReturnResult<PolishDepartmentDto>();
            try
            {
                string SqlQuery = "GetPolish";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    polish.result = connection.Query<PolishDepartmentDto>(SqlQuery, commandType: CommandType.StoredProcedure).AsList();

                }
                polish.Flag = ApplicationConstants.successFlag;
                polish.message = "Data Fetched Successfully";
                return polish;


            }
            catch (Exception ex)
            {
                polish.Flag = ApplicationConstants.failureFlag;
                polish.message = ex.ToString();
                return polish;
            }
        }

        public async Task<SingleReturnResult<PolishDepartmentDto>> GetPolishJobWithId(int Id)
        {
            SingleReturnResult<PolishDepartmentDto> polish = new SingleReturnResult<PolishDepartmentDto>();
            try
            {
                string SqlQuery = "GetPolish";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    polish.result = await connection.QueryFirstOrDefaultAsync<PolishDepartmentDto>(SqlQuery, new { PolishId = Id } , commandType:CommandType.StoredProcedure);
                }
                polish.Flag = ApplicationConstants.successFlag;
                polish.message = "Data Fetched Successfully";
                return polish;

            }
            catch (Exception ex)
            {
                polish.Flag = ApplicationConstants.failureFlag;
                polish.message = ex.ToString();
                return polish;
            }
        }

        public async Task<ListReturnResult<AssignedJobDTO>> GetPolishAssignedJob()
        {
            ListReturnResult<AssignedJobDTO> polish = new ListReturnResult<AssignedJobDTO>();
            try
            {
                string SqlQuery = "GetAssignedJob";
                var values = new { StatusId = 6 };
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    polish.result = connection.Query<AssignedJobDTO>(SqlQuery, values, commandType: CommandType.StoredProcedure).AsList();
                }
                polish.Flag = ApplicationConstants.successFlag;
                polish.message = "Data Fetched successfully";
                return polish;
            }
            catch (Exception ex)
            {
                polish.Flag = ApplicationConstants.failureFlag;
                polish.message = ex.ToString();
                return polish;
            }
        }

    }
         
}
