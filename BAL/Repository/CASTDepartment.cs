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
    public class CASTDepartment : ICASTDepartment
    {
        private readonly IDbConnections _conn;
        public CASTDepartment(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddCAST(List<CASTDepartmentDto> cast)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                DataTable dtCAST = _conn.ToDataTable(cast);
                dtCAST.Columns.Remove("CastId");

                object stat = _conn.ExecuteProcedure("InsertUpdateCAST", new SqlParameter("CASTDetail", dtCAST));
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

        public async Task<ListReturnResult<CASTDepartmentDto>> GetAllCAST()
        {
            ListReturnResult<CASTDepartmentDto> cast = new ListReturnResult<CASTDepartmentDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Department_CAST";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    cast.result = connection.Query<CASTDepartmentDto>(SqlQuery).AsList();

                }
                cast.Flag = ApplicationConstants.successFlag;
                cast.message = "Data Fetched Successfully";
                return cast;

            }
            catch (Exception ex)
            {
                cast.Flag = ApplicationConstants.failureFlag;
                cast.message = ex.ToString();
                return cast;
            }
        }

        public async Task<SingleReturnResult<CASTDepartmentDto>> GetCAST(int Id)
        {
            SingleReturnResult<CASTDepartmentDto> cast = new SingleReturnResult<CASTDepartmentDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Department_CAST WHERE CastId = @CastId";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    cast.result = await connection.QueryFirstOrDefaultAsync<CASTDepartmentDto>(SqlQuery, new { CastId = Id });
                }
                cast.Flag = ApplicationConstants.successFlag;
                cast.message = "Data Fetched Successfully";
                return cast;

            }
            catch (Exception ex)
            {
                cast.Flag = ApplicationConstants.failureFlag;
                cast.message = ex.ToString();
                return cast;
            }
        }
    }
}
