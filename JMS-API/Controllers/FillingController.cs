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
        public async Task<IActionResult> AddFilling([FromBody]FillingDepartmentDto filling)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newfilling = await _filling.AddFilling(filling);
            response = Ok(newfilling);
            return response;
        }


        [HttpPost]
        [Route("UpdateFilling")]
        public async Task<IActionResult> UpdateFilling([FromBody]FillingDepartmentDto filling)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newfilling = await _filling.UpdateFilling(filling);
            response = Ok(newfilling);
            return response;
        }

        [HttpGet]
        [Route("FillingDetails")]
        public async Task<IActionResult> FillingDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<FillingDepartmentDto> fillingDetails = await _filling.GetFillingJobs();
            response = Ok(fillingDetails);
            return response;
        }

        [HttpGet]
        [Route("FillingJobById")]
        public async Task<IActionResult> FillingJobById(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<FillingDepartmentDto> fillingDetail = await _filling.GetFillingJobWithId(id);
            response = Ok(fillingDetail);
            return response;
        }

        [HttpGet]
        [Route("JobForFilling")]
        public async Task<IActionResult> JobForFilling()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<AssignedJobDTO> hrDetails = await _filling.GetFillingAssignedJob();
            response = Ok(hrDetails);
            return response;
        }

        [HttpGet]
        [Route("GetFillingReport")]
        public async Task<IActionResult> GetFillingReport(int jobId ,int employeeId ,string fromDate ,string toDate , int status)
        {
            IActionResult response = Unauthorized();
            ListReturnResult<FillingReportDto> result = await _filling.GetFillingReport(jobId , employeeId ,fromDate , toDate ,status);
            response = Ok(result);
            return response;
        }

    }
}