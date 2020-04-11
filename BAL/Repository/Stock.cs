using BLL.Interface;
using Dapper;
using DTO.DTOModels;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class Stock : IStock
    {
        private readonly IDbConnections _conn;
        public Stock(IDbConnections con)
        {
            _conn = con;
        }
//test
        public async Task<SingleReturnResult<StockDto>> AddStock(StockDto stock)
        {
            SingleReturnResult<StockDto> result = new SingleReturnResult<StockDto>();
            StockDto stockDto = new StockDto();
            try
            {
                var procResult = _conn.ExecuteProcedure("InsertUpdateStock", new SqlParameter("ProcType", "INSERT")
                                                                                , new SqlParameter("StockId", stock.StockId)
                                                                                , new SqlParameter("JobId", stock.JobId)
                                                                                , new SqlParameter("StockDate", stock.StockDate)
                                                                                , new SqlParameter("StockWeight", stock.StockWeight)
                                                                                , new SqlParameter("Purity ", stock.Purity)
                                                                                , new SqlParameter("Remark", stock.Remark)
                                                                                , new SqlParameter("AddedBy", stock.AddedBy)
                                                                                , new SqlParameter("ModifiedBy", stock.ModifiedBy));

                if (procResult != null)
                {
                    result.Flag = ApplicationConstants.successFlag;
                    result.result = stockDto;
                    result.message = "Stock has been added !";
                }
                else
                {
                    result.Flag = ApplicationConstants.failureFlag;
                    result.result = stockDto;
                    result.message = "Some error has occured while adding stock...Please try again after sometime.";
                }
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                result.Flag = ApplicationConstants.failureFlag;
                result.result = stockDto;
                result.message = "Some error has occured while adding stock...Please try again after sometime." + ex.ToString();
                return result;
            }

        }
        public async Task<SingleReturnResult<StockDto>> UpdateStock(StockDto stock)
        {
            SingleReturnResult<StockDto> result = new SingleReturnResult<StockDto>();
            StockDto stockDto = new StockDto();
            try
            {
                var procResult = _conn.ExecuteProcedure("InsertUpdateStock", new SqlParameter("ProcType", "UPDATE")
                                                                                 , new SqlParameter("StockId", stock.StockId)
                                                                                 , new SqlParameter("JobId", stock.JobId)
                                                                                 , new SqlParameter("StockDate", stock.StockDate)
                                                                                 , new SqlParameter("StockWeight", stock.StockWeight)
                                                                                 , new SqlParameter("Purity ", stock.Purity)
                                                                                 , new SqlParameter("Remark", stock.Remark)
                                                                                 , new SqlParameter("AddedBy", stock.AddedBy)
                                                                                 , new SqlParameter("ModifiedBy", stock.ModifiedBy));

                if (procResult != null)
                {
                    result.Flag = ApplicationConstants.successFlag;
                    result.result = stockDto;
                    result.message = "Stock Details have been updated !";
                }
                else
                {
                    result.Flag = ApplicationConstants.failureFlag;
                    result.result = stockDto;
                    result.message = "Some error has occured while updating stock...Please try again after sometime.";
                }
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                result.Flag = ApplicationConstants.failureFlag;
                result.result = stockDto;
                result.message = "Some error has occured while adding stock...Please try again after sometime." + ex.ToString();
                return result;
            }
        }

        public async Task<ListReturnResult<StockDto>> GetStockList()
        {
            ListReturnResult<StockDto> stock = new ListReturnResult<StockDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Stock";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    stock.result = connection.Query<StockDto>(SqlQuery).AsList();
                }
                if (stock.result != null)
                {
                    stock.Flag = ApplicationConstants.successFlag;
                    stock.message = "Data Fetched successfully";
                }
                else
                {
                    stock.Flag = ApplicationConstants.failureFlag;
                    stock.message = "No Records found !";
                }
                return stock;
            }
            catch (Exception ex)
            {
                stock.Flag = ApplicationConstants.failureFlag;
                stock.message = ex.ToString();
                return stock;
            }
        }

        public async Task<SingleReturnResult<StockDto>> GetStockById(int Id)
        {
            SingleReturnResult<StockDto> stock = new SingleReturnResult<StockDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Stock WHERE StockId = @StockId";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    stock.result = await connection.QueryFirstOrDefaultAsync<StockDto>(SqlQuery, new { StockId = Id });
                }

                if (stock.result != null)
                {
                    stock.Flag = ApplicationConstants.successFlag;
                    stock.message = "Data Fetched Successfully!";
                }
                else
                {
                    stock.Flag = ApplicationConstants.failureFlag;
                    stock.message = "No Records found !";
                }
                return stock;
            }
            catch (Exception ex)
            {
                stock.Flag = ApplicationConstants.failureFlag;
                stock.message = ex.ToString();
                return stock;
            }
        }
    }
}
