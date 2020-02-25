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

        public async Task<SingleReturnResult<string>> AddPolish(List<PolishDepartmentDto> polish)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                DataTable dtPolish = _conn.ToDataTable(polish);
                dtPolish.Columns.Remove("PolishId");

                object stat = _conn.ExecuteProcedure("InsertUpdatePolish", new SqlParameter("PolishDetail", dtPolish));
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

        public async Task<ListReturnResult<PolishDepartmentDto>> GetAllPolish()
        {
            ListReturnResult<PolishDepartmentDto> polish = new ListReturnResult<PolishDepartmentDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Department_Polish";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    polish.result = connection.Query<PolishDepartmentDto>(SqlQuery).AsList();

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

        public async Task<SingleReturnResult<PolishDepartmentDto>> GetPolish(int Id)
        {
            SingleReturnResult<PolishDepartmentDto> polish = new SingleReturnResult<PolishDepartmentDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Department_Polish WHERE PolishId = @PolishId";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    polish.result = await connection.QueryFirstOrDefaultAsync<PolishDepartmentDto>(SqlQuery, new { PolishId = Id });
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

    }
         
}
