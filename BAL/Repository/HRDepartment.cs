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
    public class HRDepartment : IHRDepartment
    {
        private readonly IDbConnections _conn;
        public HRDepartment(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddUpdateHR(List<HRDepartmentDto> hr)
        {

            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                DataTable dtHRDept = _conn.ToDataTable(hr);
                dtHRDept.Columns.Remove("JobNo");
                dtHRDept.Columns.Remove("Department");
                dtHRDept.Columns.Remove("Employee");

        object stat = _conn.ExecuteProcedure("InsertUpdateHRDepartment", new SqlParameter("HRDepartment", dtHRDept));
                                                                                   
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

        public async Task<ListReturnResult<JobMasterDto>> GetHRAssignedJob()
        {
            ListReturnResult<JobMasterDto> hr = new ListReturnResult<JobMasterDto>();
            try
            {
                string SqlQuery = "SELECT JobId,JobNo FROM JobMaster WHERE ProcessStatus = 0";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    hr.result = connection.Query<JobMasterDto>(SqlQuery).AsList();
                }
                hr.Flag = ApplicationConstants.successFlag;
                hr.message = "Data Fetched successfully";
                return hr;
            }
            catch (Exception ex)
            {
                hr.Flag = ApplicationConstants.failureFlag;
                hr.message = ex.ToString();
                return hr;
            }
        }

        public async Task<ListReturnResult<HRDepartmentDto>> GetHRDepartmentJobs()
        {
            ListReturnResult<HRDepartmentDto> hrDept = new ListReturnResult<HRDepartmentDto>();
            try
            {
                string SqlProc = "GetHR";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    hrDept.result = connection.Query<HRDepartmentDto>(SqlProc , commandType: CommandType.StoredProcedure).AsList();
                }
                hrDept.Flag = ApplicationConstants.successFlag;
                hrDept.message = "Data Fetched successfully";
                return hrDept;
            }
            catch (Exception ex)
            {
                hrDept.Flag = ApplicationConstants.failureFlag;
                hrDept.message = ex.ToString();
                return hrDept;
            }
        }
    }
}
