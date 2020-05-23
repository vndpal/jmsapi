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
    public class DeliveryScreen : IDeliveryScreen
    {
        private readonly IDbConnections _conn;
        public DeliveryScreen(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddDeliveryScreen(DeliveryScreenDto del)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {

                var procResult = _conn.ExecuteProcedure("InsertDeliveryScreen", new SqlParameter("ProcType", "INSERT")
                                                                             , new SqlParameter("JobId", del.JobId)
                                                                             , new SqlParameter("DeliveryDate", del.DeliveryDate)
                                                                             , new SqlParameter("BookNo", del.BookNo)
                                                                             , new SqlParameter("GrossWeight ", del.GrossWeight)
                                                                             , new SqlParameter("NetWeightPure", del.NetWeightPure)
                                                                             , new SqlParameter("NetWeight24K", del.NetWeight24K)
                                                                             , new SqlParameter("Remark", del.Remark));
                if (procResult != null)
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

        public async Task<ListReturnResult<AssignedJobDTO>> GetDeliveryAssignedJob()
        {
            ListReturnResult<AssignedJobDTO> del = new ListReturnResult<AssignedJobDTO>();
            try
            {
                string SqlQuery = "GetAssignedJob";
                var values = new { StatusId = 10 };
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    del.result = connection.Query<AssignedJobDTO>(SqlQuery, values, commandType: CommandType.StoredProcedure).AsList();
                }
                del.Flag = ApplicationConstants.successFlag;
                del.message = "Data Fetched successfully";
                return del;
            }
            catch (Exception ex)
            {
                del.Flag = ApplicationConstants.failureFlag;
                del.message = ex.ToString();
                return del;
            }
        }

       
    }
}
