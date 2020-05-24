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
    }
}
