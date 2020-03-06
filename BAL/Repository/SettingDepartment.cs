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
    public class SettingDepartment : ISettingDepartment
    {
        private readonly IDbConnections _conn;
        public SettingDepartment(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddSetting(List<SettingDepartmentDto> set)
        {

            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                DataTable dtSetDept = _conn.ToDataTable(set[0].SetDiamond);

                object stat = _conn.ExecuteProcedure("InsertUpdateSetDepartment", new SqlParameter("JobId", set[0].JobId),
                                                                                  new SqlParameter("EmployeeId", set[0].EmployeeId),
                                                                                  new SqlParameter("IssuedDate", set[0].IssuedDate),
                                                                                  new SqlParameter("ReceivedDate", set[0].ReceivedDate),
                                                                                  new SqlParameter("IssuedWeight", set[0].IssuedWeight),
                                                                                  new SqlParameter("ReceivedWeight", set[0].ReceivedWeight),
                                                                                  new SqlParameter("DiamondWeight", set[0].DiamondWeight),
                                                                                  new SqlParameter("Loss", set[0].Loss),
                                                                                  new SqlParameter("Remark", set[0].Remark),
                                                                                  new SqlParameter("SetDepartment", dtSetDept));

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

        public async Task<ListReturnResult<SettingDepartmentDto>> GetAllSetting()
        {
            ListReturnResult<SettingDepartmentDto> set = new ListReturnResult<SettingDepartmentDto>();
            try
            {
                string SqlProc = "GetSetting";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    set.result = connection.Query<SettingDepartmentDto>(SqlProc, commandType: CommandType.StoredProcedure).AsList();

                }
                set.Flag = ApplicationConstants.successFlag;
                set.message = "Data Fetched Successfully";
                return set;

            }
            catch (Exception ex)
            {
                set.Flag = ApplicationConstants.failureFlag;
                set.message = ex.ToString();
                return set;
            }
        }

        public SingleReturnResult<JobMasterDto> GetSettingAssignedJob()
        {
            SingleReturnResult<JobMasterDto> set = new SingleReturnResult<JobMasterDto>();
            try
            {
                DataSet dsSetJobs = _conn.ExecuteProcedureForDataSet("GetSettingJobs");
                List<JobMasterDto> jobSetDetail = new List<JobMasterDto>();

                jobSetDetail = _conn.ConvertDataTable<JobMasterDto>(dsSetJobs.Tables[0]);
                if (dsSetJobs.Tables.Count > 1)
                {
                    jobSetDetail[0].diamondDetail = new List<DiamondDetailDto>();
                    jobSetDetail[0].diamondDetail = _conn.ConvertDataTable<DiamondDetailDto>(dsSetJobs.Tables[1]);
                }

                set.result = jobSetDetail[0];
                set.Flag = ApplicationConstants.successFlag;
                set.message = "Data Fetched successfully";
                return set;
            }
            catch (Exception ex)
            {
                set.Flag = ApplicationConstants.failureFlag;
                set.message = ex.ToString();
                return set;
            }
        }

    }
}
