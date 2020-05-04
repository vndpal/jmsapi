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

        public async Task<SingleReturnResult<string>> AddSetting(SettingDepartmentDto set)
        {

            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                DataTable dtSetDept = _conn.ToDataTable(set.SetDiamond);
                dtSetDept.Columns.Remove("DiamondDate");
                dtSetDept.Columns.Remove("DiamondType");
                dtSetDept.Columns.Remove("DiamondTypeValue");
                dtSetDept.Columns.Remove("IssuedPiece");
                dtSetDept.Columns.Remove("IssuedWeight");
                dtSetDept.Columns.Remove("SettingId");

                object stat = _conn.ExecuteProcedure("InsertUpdateSetDepartment", new SqlParameter("ProcType", "INSERT"),
                                                                                  new SqlParameter("SettingId", 0),
                                                                                  new SqlParameter("JobId", set.JobId),
                                                                                  new SqlParameter("EmployeeId", set.EmployeeId),
                                                                                  new SqlParameter("IssuedDate", set.IssuedDate),
                                                                                  new SqlParameter("ReceivedDate", set.ReceivedDate),
                                                                                  new SqlParameter("IssuedWeight", set.IssuedWeight),
                                                                                  new SqlParameter("ReceivedWeight", set.ReceivedWeight),
                                                                                  new SqlParameter("DiamondWeight", set.DiamondWeight),
                                                                                  new SqlParameter("Loss", set.Loss),
                                                                                  new SqlParameter("Remark", set.Remark),
                                                                                  new SqlParameter("Status", set.Status),
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

        public async Task<SingleReturnResult<string>> UpdateSetting(SettingDepartmentDto set)
        {

            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                DataTable dtSetDept = _conn.ToDataTable(set.SetDiamond);
                dtSetDept.Columns.Remove("DiamondDate");
                dtSetDept.Columns.Remove("DiamondType");
                dtSetDept.Columns.Remove("DiamondTypeValue");
                dtSetDept.Columns.Remove("IssuedPiece");
                dtSetDept.Columns.Remove("IssuedWeight");
                dtSetDept.Columns.Remove("SettingId");

                object stat = _conn.ExecuteProcedure("InsertUpdateSetDepartment", new SqlParameter("ProcType", "Update"),
                                                                                  new SqlParameter("SettingId", set.SettingId),
                                                                                  new SqlParameter("JobId", set.JobId),
                                                                                  new SqlParameter("EmployeeId", set.EmployeeId),
                                                                                  new SqlParameter("IssuedDate", set.IssuedDate),
                                                                                  new SqlParameter("ReceivedDate", set.ReceivedDate),
                                                                                  new SqlParameter("IssuedWeight", set.IssuedWeight),
                                                                                  new SqlParameter("ReceivedWeight", set.ReceivedWeight),
                                                                                  new SqlParameter("DiamondWeight", set.DiamondWeight),
                                                                                  new SqlParameter("Loss", set.Loss),
                                                                                  new SqlParameter("Remark", set.Remark),
                                                                                  new SqlParameter("Status", set.Status),
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

        public async Task<ListReturnResult<SettingDepartmentDto>> GetSettingJobs()
        {
            ListReturnResult<SettingDepartmentDto> set = new ListReturnResult<SettingDepartmentDto>();
            try
            {
                string SqlQuery = "GetSetting";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    set.result = connection.Query<SettingDepartmentDto>(SqlQuery, commandType: CommandType.StoredProcedure).AsList();

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

        public async Task<SingleReturnResult<SettingDepartmentDto>> GetSettingJobWithId(int Id)
        {
            
            SingleReturnResult<SettingDepartmentDto> set = new SingleReturnResult<SettingDepartmentDto>();
            List<SettingDepartmentDto> tempset = new List<SettingDepartmentDto>();
            try
            {
                DataSet dsSetting = await _conn.ExecuteProcedureForDataSet("GetSetting",new SqlParameter("SettingId",Id));
                tempset = _conn.ConvertDataTable<SettingDepartmentDto>(dsSetting.Tables[0]);
                if (dsSetting.Tables[1].Rows.Count > 0)
                {
                    tempset[0].SetDiamond = new List<SetDiamondsDto>();
                    tempset[0].SetDiamond = _conn.ConvertDataTable<SetDiamondsDto>(dsSetting.Tables[1]);
                }
                set.result = tempset[0];
                set.result.SetDiamond = tempset[0].SetDiamond;
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

        public async Task<ListReturnResult<AssignedJobDTO>> GetSettingAssignedJob()
        {
            ListReturnResult<AssignedJobDTO> set = new ListReturnResult<AssignedJobDTO>();
            try
            {
                string SqlQuery = "GetAssignedJob";
                var values = new { StatusId = 8 };
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    set.result = connection.Query<AssignedJobDTO>(SqlQuery, values, commandType: CommandType.StoredProcedure).AsList();
                }
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

        public async Task<ListReturnResult<DiamondDetailDto>> GetStoneForFitter(int Id)
        {
            ListReturnResult<DiamondDetailDto> set = new ListReturnResult<DiamondDetailDto>();
            try
            {
                string SqlQuery = "GetStoneForFitter";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    set.result = connection.Query<DiamondDetailDto>(SqlQuery,new {JobId = Id }, commandType: CommandType.StoredProcedure).AsList();

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

    }
}
