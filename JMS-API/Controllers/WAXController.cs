using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WAXController : Controller
    {
        private readonly IWAXDepartment _wax;
        public WAXController(IWAXDepartment wax)
        {
            _wax = wax;
        }

        [HttpPost]
        [Route("AddWAX")]
        public async Task<IActionResult> AddWAX([FromBody]List<WAXDepartmentDto> wax)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newwax = await _wax.AddWAX(wax);
            response = Ok(newwax);
            return response;
        }

        [HttpGet]
        [Route("WAXDetails")]
        public async Task<IActionResult> WAXDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<WAXDepartmentDto> waxDetails = await _wax.GetAllWAX();
            response = Ok(waxDetails);
            return response;
        }

        [HttpGet("{id}")]
        [Route("WAXDetail")]
        public async Task<IActionResult> WAXDetail(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<WAXDepartmentDto> waxDetail = await _wax.GetWAX(id);
            response = Ok(waxDetail);
            return response;
        }
    }
}