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
    public class CADDepartment : ICADDepartment
    {
        private readonly IDbConnections _conn;
        public CADDepartment(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddCAD(List<CADDepartmentDto> cad)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                DataTable dtCAD = _conn.ToDataTable(cad);
                dtCAD.Columns.Remove("CADId");
                dtCAD.Columns.Remove("JobNo");
                dtCAD.Columns.Remove("Employee");

                object stat = _conn.ExecuteProcedure("InsertUpdateCAD", new SqlParameter("CADDetails", dtCAD));
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

        public async Task<ListReturnResult<CADDepartmentDto>> GetAllCAD()
        {
            ListReturnResult<CADDepartmentDto> cad = new ListReturnResult<CADDepartmentDto>();
            try
            {
                string SqlProc = "GetCAD";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    cad.result = connection.Query<CADDepartmentDto>(SqlProc , commandType: CommandType.StoredProcedure).AsList();

                }
                cad.Flag = ApplicationConstants.successFlag;
                cad.message = "Data Fetched Successfully";
                return cad;

            }
            catch (Exception ex)
            {
                cad.Flag = ApplicationConstants.failureFlag;
                cad.message = ex.ToString();
                return cad;
            }
        }

        public async Task<SingleReturnResult<CADDepartmentDto>> GetCAD(int Id)
        {
            SingleReturnResult<CADDepartmentDto> cad = new SingleReturnResult<CADDepartmentDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Department_CAD WHERE CADId = @CADId";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    cad.result = await connection.QueryFirstOrDefaultAsync<CADDepartmentDto>(SqlQuery, new { CADId = Id });
                }
                cad.Flag = ApplicationConstants.successFlag;
                cad.message = "Data Fetched Successfully";
                return cad;

            }
            catch (Exception ex)
            {
                cad.Flag = ApplicationConstants.failureFlag;
                cad.message = ex.ToString();
                return cad;
            }
        }

        public async Task<ListReturnResult<JobMasterDto>> GetCADAssignedJob()
        {
            ListReturnResult<JobMasterDto> cad = new ListReturnResult<JobMasterDto>();
            try
            {
                string SqlQuery = "SELECT JobId,JobNo FROM JobMaster WHERE ProcessStatus = 1";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    cad.result = connection.Query<JobMasterDto>(SqlQuery).AsList();
                }
                cad.Flag = ApplicationConstants.successFlag;
                cad.message = "Data Fetched successfully";
                return cad;
            }
            catch (Exception ex)
            {
                cad.Flag = ApplicationConstants.failureFlag;
                cad.message = ex.ToString();
                return cad;
            }
        }

        //public async Task<SingleReturnResult<CADDepartmentDto>> UpdateCAD(CADDepartmentDto caddetails)
        //{
        //    SingleReturnResult<CADDepartmentDto> cad = new SingleReturnResult<CADDepartmentDto>();
        //    try
        //    {
        //        string SqlQuery = "UPDATE Department_CAD SET JobId=@JobId ,IssueDate=@IssueDate ,ReceivedDate=@ReceivedDate," +
        //                          "PieceQuantity=@PieceQuantity,ResinType=@ResinType,RPTWeight=@RPTWeight,RPTRate=@RPTRate," +
        //                          "Amount=@Amount,Remark=@Remark,ModifiedOn=@ModifiedOn,ModifiedBy=@ModifiedBy,Status=@Status" +
        //                          "WHERE CADId=@CADId";

        //        using (var connection = new SqlConnection(_conn.strConnectionString()))
        //        {
        //            await connection.OpenAsync();
        //            cad.result = await connection.QueryFirstOrDefaultAsync<CADDepartmentDto>(SqlQuery, new
        //            {
        //                CADId = caddetails.CADId,
        //                JobId = caddetails.JobId,
        //                IssueDate = caddetails.IssueDate,
        //                ReceivedDate = caddetails.ReceivedDate,
        //                PieceQuantity = caddetails.PieceQuantity,
        //                ResinType = caddetails.ResinType,
        //                RPTWeight = caddetails.RPTWeight,
        //                RPTRate = caddetails.RPTRate,
        //                Amount = caddetails.Amount,
        //                Remark = caddetails.Remark,
        //                Status = caddetails.Status
        //            });

        //        }
        //        cad.Flag = ApplicationConstants.successFlag;
        //        cad.message = "Data Fetched Successfully!";
        //        return cad;
        //    }
        //    catch (Exception ex)
        //    {
        //        cad.Flag = ApplicationConstants.failureFlag;
        //        cad.message = ex.ToString();
        //        return cad;
        //    }
       // }
    }
}
