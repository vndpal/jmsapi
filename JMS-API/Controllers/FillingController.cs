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
    public class FillingController : Controller
    {
        private readonly IFillingDepartment _filling;
        public FillingController(IFillingDepartment filling)
        {
            _filling = filling;
        }

        [HttpPost]
        [Route("AddFilling")]
        public async Task<IActionResult> AddFilling([FromBody]List<FillingDepartmentDto> filling)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newfilling = await _filling.AddFilling(filling);
            response = Ok(newfilling);
            return response;
        }

        [HttpGet]
        [Route("FillingDetails")]
        public async Task<IActionResult> FillingDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<FillingDepartmentDto> fillingDetails = await _filling.GetAllFilling();
            response = Ok(fillingDetails);
            return response;
        }

        [HttpGet("{id}")]
        [Route("FillingDetail")]
        public async Task<IActionResult> FillingDetail(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<FillingDepartmentDto> fillingDetail = await _filling.GetFilling(id);
            response = Ok(fillingDetail);
            return response;
        }
    }
}