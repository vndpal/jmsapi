using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using DTO.DTOModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStock _stock;
        public StockController(IStock stock)
        {
            _stock = stock;
        }

        [HttpPost]
        [Route("AddStock")]
        public async Task<IActionResult> AddStock([FromBody] StockDto stock)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<StockDto> result = await _stock.AddStock(stock);
            response = Ok(result);
            return response;
        }

        [HttpPost]
        [Route("UpdateStock")]
        public async Task<IActionResult> UpdateStock([FromBody] StockDto stock)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<StockDto> result = await _stock.UpdateStock(stock);
            response = Ok(result);
            return response;
        }

        [HttpGet]
        [Route("GetStockList")]
        public async Task<IActionResult> GetStockList()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<StockDto> result = await _stock.GetStockList();
            response = Ok(result);
            return response;
        }

        [HttpGet]
        [Route("GetStockById")]
        public async Task<IActionResult> GetStockById(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<StockDto> result = await _stock.GetStockById(id);
            response = Ok(result);
            return response;
        }


        [HttpGet]
        [Route("GetBalancedWeight")]
        public async Task<IActionResult> GetBalancedWeight()
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<decimal> result = await _stock.GetBalancedWeight();
            response = Ok(result);
            return response;
        }
    }
}