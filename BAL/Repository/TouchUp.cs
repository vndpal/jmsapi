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
    public class TouchUp : ITouchUp
    {
        private readonly IDbConnections _conn;
        public TouchUp(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddTouchUp(TouchUpDto touch)
        {

            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                DataTable dtTouchUp = _conn.ToDataTable(touch.TouchUpDetail);
                dtTouchUp.Columns.Remove("DiamondDate");
                dtTouchUp.Columns.Remove("DiamondType");
                dtTouchUp.Columns.Remove("DiamondTypeValue");
                dtTouchUp.Columns.Remove("IssuedPiece");
                dtTouchUp.Columns.Remove("IssuedWeight");
                dtTouchUp.Columns.Remove("SettingId");

                object stat = _conn.ExecuteProcedure("InsertUpdateTouchUp", new SqlParameter("ProcType", "INSERT"),
                                                                                  new SqlParameter("JobId", touch.JobId),
                                                                                  new SqlParameter("TouchUpDate", touch.TouchUpDate),
                                                                                  new SqlParameter("MeenaType", touch.MeenaType),
                                                                                  new SqlParameter("MeenaWeight", touch.MeenaWeight),
                                                                                  new SqlParameter("MeenaAmount", touch.MeenaAmount),
                                                                                  new SqlParameter("PaintingWeight", touch.PaintingWeight),
                                                                                  new SqlParameter("PaintingAmount", touch.PaintingAmount),
                                                                                  new SqlParameter("MotiAmount", touch.MotiAmount),
                                                                                  new SqlParameter("IssueChainWeight", touch.IssueChainWeight),
                                                                                  new SqlParameter("UsedChainWeight", touch.UsedChainWeight),
                                                                                  new SqlParameter("BalanceChainWeight", touch.BalanceChainWeight),
                                                                                  new SqlParameter("Remark", touch.Remark),
                                                                                  new SqlParameter("TouchUpDetail", dtTouchUp));

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

        public async Task<ListReturnResult<AssignedJobDTO>> GetTouchUpAssignedJob()
        {
            ListReturnResult<AssignedJobDTO> touch = new ListReturnResult<AssignedJobDTO>();
            try
            {
                string SqlQuery = "GetAssignedJob";
                var values = new { StatusId = 9 };
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    touch.result = connection.Query<AssignedJobDTO>(SqlQuery, values, commandType: CommandType.StoredProcedure).AsList();
                }
                touch.Flag = ApplicationConstants.successFlag;
                touch.message = "Data Fetched successfully";
                return touch;
            }
            catch (Exception ex)
            {
                touch.Flag = ApplicationConstants.failureFlag;
                touch.message = ex.ToString();
                return touch;
            }
        }

        public async Task<ListReturnResult<DiamondDetailDto>> GetStoneForTouchUp(int Id)
        {
            ListReturnResult<DiamondDetailDto> touch = new ListReturnResult<DiamondDetailDto>();
            try
            {
                string SqlQuery = "GetStoneForFitter";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    touch.result = connection.Query<DiamondDetailDto>(SqlQuery, new { JobId = Id }, commandType: CommandType.StoredProcedure).AsList();

                }
                touch.Flag = ApplicationConstants.successFlag;
                touch.message = "Data Fetched Successfully";
                return touch;


            }
            catch (Exception ex)
            {
                touch.Flag = ApplicationConstants.failureFlag;
                touch.message = ex.ToString();
                return touch;
            }
        }
    }
}
