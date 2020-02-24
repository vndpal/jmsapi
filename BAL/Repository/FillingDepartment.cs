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

        public async Task<SingleReturnResult<string>> AddFilling(List<FillingDepartmentDto> filling)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                DataTable dtFilling = _conn.ToDataTable(filling);
                dtFilling.Columns.Remove("FillingId");

                object stat = _conn.ExecuteProcedure("InsertUpdateFilling", new SqlParameter("FillingDetail", dtFilling));
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

        public async Task<ListReturnResult<FillingDepartmentDto>> GetAllFilling()
        {
            ListReturnResult<FillingDepartmentDto> filling = new ListReturnResult<FillingDepartmentDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Department_Filling";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    filling.result = connection.Query<FillingDepartmentDto>(SqlQuery).AsList();

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

        public async Task<SingleReturnResult<FillingDepartmentDto>> GetFilling(int Id)
        {
            SingleReturnResult<FillingDepartmentDto> filling = new SingleReturnResult<FillingDepartmentDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Department_Filling WHERE FillingId = @FillingId";
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    filling.result = await connection.QueryFirstOrDefaultAsync<FillingDepartmentDto>(SqlQuery, new { FillingId = Id });
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
    }
}
