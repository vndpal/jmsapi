using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IStock
    {
        Task<SingleReturnResult<StockDto>> AddStock(StockDto stock);
        Task<SingleReturnResult<StockDto>> UpdateStock(StockDto inventory);
        Task<ListReturnResult<StockDto>> GetStockList();
        Task<SingleReturnResult<StockDto>> GetStockById(int id);
    }
}
