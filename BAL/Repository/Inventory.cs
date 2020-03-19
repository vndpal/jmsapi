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
    public class Inventory : IInventory
    {
        private readonly IDbConnections _conn;
        public Inventory(IDbConnections con)
        {
            _conn = con;
        }

        public async Task<SingleReturnResult<InventoryDto>> AddInventory(InventoryDto inventory)
        {
            SingleReturnResult<InventoryDto> result = new SingleReturnResult<InventoryDto>();
            InventoryDto inventoryDto = new InventoryDto();
            try
            {
                var procResult = _conn.ExecuteProcedure("InsertUpdateInventory",  new SqlParameter("ProcType", "INSERT")
                                                                                , new SqlParameter("InvId", inventory.InvId)
                                                                                , new SqlParameter("InvDate", inventory.InvDate)
                                                                                , new SqlParameter("Company", inventory.Company)
                                                                                , new SqlParameter("MetalType ", inventory.MetalType)
                                                                                , new SqlParameter("MetalWeight", inventory.MetalWeight)
                                                                                , new SqlParameter("Hallmark", inventory.Hallmark)
                                                                                , new SqlParameter("Purity", inventory.Purity)
                                                                                , new SqlParameter("Remark", inventory.Remark)
                                                                                , new SqlParameter("AddedBy", inventory.AddedBy)
                                                                                , new SqlParameter("ModifiedBy", inventory.ModifiedBy));

                if (procResult != null)
                {
                    result.Flag = ApplicationConstants.successFlag;
                    result.result = inventoryDto;
                    result.message = "Inventory has been added !";
                }
                else
                {
                    result.Flag = ApplicationConstants.failureFlag;
                    result.result = inventoryDto;
                    result.message = "Some error has occured while adding inventory...Please try again after sometime.";
                }
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                result.Flag = ApplicationConstants.failureFlag;
                result.result = inventoryDto;
                result.message = "Some error has occured while adding inventory...Please try again after sometime." + ex.ToString();
                return result;
            }

        }
        public async Task<SingleReturnResult<InventoryDto>> UpdateInventory(InventoryDto inventory)
        {
            SingleReturnResult<InventoryDto> result = new SingleReturnResult<InventoryDto>();
            InventoryDto inventoryDto = new InventoryDto();
            try
            {
                var procResult = _conn.ExecuteProcedure("InsertUpdateInventory", new SqlParameter("ProcType", "UPDATE")
                                                                                , new SqlParameter("InvId", inventory.InvId)
                                                                                , new SqlParameter("InvDate", inventory.InvDate)
                                                                                , new SqlParameter("Company", inventory.Company)
                                                                                , new SqlParameter("MetalType ", inventory.MetalType)
                                                                                , new SqlParameter("MetalWeight", inventory.MetalWeight)
                                                                                , new SqlParameter("Hallmark", inventory.Hallmark)
                                                                                , new SqlParameter("Purity", inventory.Purity)
                                                                                , new SqlParameter("Remark", inventory.Remark)
                                                                                , new SqlParameter("AddedBy", inventory.AddedBy)
                                                                                , new SqlParameter("ModifiedBy", inventory.ModifiedBy));

                if (procResult != null)
                {
                    result.Flag = ApplicationConstants.successFlag;
                    result.result = inventoryDto;
                    result.message = "Inventory Details have been updated !";
                }
                else
                {
                    result.Flag = ApplicationConstants.failureFlag;
                    result.result = inventoryDto;
                    result.message = "Some error has occured while updating inventory...Please try again after sometime.";
                }
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                result.Flag = ApplicationConstants.failureFlag;
                result.result = inventoryDto;
                result.message = "Some error has occured while adding inventory...Please try again after sometime." + ex.ToString();
                return result;
            }
        }

        public async Task<ListReturnResult<InventoryDto>> GetInventoryList()
        {
            ListReturnResult<InventoryDto> inv = new ListReturnResult<InventoryDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Inventory";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    inv.result = connection.Query<InventoryDto>(SqlQuery).AsList();
                }
                if (inv.result != null)
                {
                    inv.Flag = ApplicationConstants.successFlag;
                    inv.message = "Data Fetched successfully";
                }
                else
                {
                    inv.Flag = ApplicationConstants.failureFlag;
                    inv.message = "No Records found !";
                }
                return inv;
            }
            catch (Exception ex)
            {
                inv.Flag = ApplicationConstants.failureFlag;
                inv.message = ex.ToString();
                return inv;
            }
        }

        public async Task<SingleReturnResult<InventoryDto>> GetInventoryById(int Id)
        {
            SingleReturnResult<InventoryDto> inv = new SingleReturnResult<InventoryDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Inventory WHERE InvId = @InvId";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    inv.result = await connection.QueryFirstOrDefaultAsync<InventoryDto>(SqlQuery, new { InvId = Id });
                }

                if (inv.result != null)
                {
                    inv.Flag = ApplicationConstants.successFlag;
                    inv.message = "Data Fetched Successfully!";
                }
                else
                {
                    inv.Flag = ApplicationConstants.failureFlag;
                    inv.message = "No Records found !";
                }
                return inv;
            }
            catch (Exception ex)
            {
                inv.Flag = ApplicationConstants.failureFlag;
                inv.message = ex.ToString();
                return inv;
            }
        }
    }
}