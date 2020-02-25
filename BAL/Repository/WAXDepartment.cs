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
    public class WAXDepartment : IWAXDepartment
    {
        private readonly IDbConnections _conn;
        public WAXDepartment(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddWAX(List<WAXDepartmentDto> wax)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                DataTable dtWAX = _conn.ToDataTable(wax);
                dtWAX.Columns.Remove("WAXId");

                object stat = _conn.ExecuteProcedure("InsertUpdateWAX", new SqlParameter("WAXDetail", dtWAX));
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

        public async Task<ListReturnResult<WAXDepartmentDto>> GetAllWAX()
        {
            ListReturnResult<WAXDepartmentDto> wax = new ListReturnResult<WAXDepartmentDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Department_WAX";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    wax.result = connection.Query<WAXDepartmentDto>(SqlQuery).AsList();

                }
                wax.Flag = ApplicationConstants.successFlag;
                wax.message = "Data Fetched Successfully";
                return wax;

            }
            catch (Exception ex)
            {
                wax.Flag = ApplicationConstants.failureFlag;
                wax.message = ex.ToString();
                return wax;
            }
        }

        public async Task<SingleReturnResult<WAXDepartmentDto>> GetWAX(int Id)
        {
            SingleReturnResult<WAXDepartmentDto> wax = new SingleReturnResult<WAXDepartmentDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Department_WAX WHERE WAXId = @WAXId";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    wax.result = await connection.QueryFirstOrDefaultAsync<WAXDepartmentDto>(SqlQuery, new { WAXId = Id });
                }
                wax.Flag = ApplicationConstants.successFlag;
                wax.message = "Data Fetched Successfully";
                return wax;

            }
            catch (Exception ex)
            {
                wax.Flag = ApplicationConstants.failureFlag;
                wax.message = ex.ToString();
                return wax;
            }
        }
            
        
    }
}
