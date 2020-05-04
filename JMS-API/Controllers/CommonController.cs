using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Interface;
using DTO.DTOModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonMaster _common;

        public CommonController(ICommonMaster common)
        {
            _common = common;
        }

        [HttpGet]
        [Route("getCommonDetails")]
        public async Task<IActionResult> GetCommonDetails(string mstGroup)
        {
            IActionResult response = Unauthorized();
            ListReturnResult<CommonMasterDto> commonDetail = await _common.getCommonDetailsAsync(mstGroup);
            response = Ok(commonDetail);
            return response;
        }

        [HttpGet]
        [Route("getJobMaster")]
        public async Task<IActionResult> GetJobMaster()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<JobMasterDto> jobData = await _common.getJobMaster();
            response = Ok(jobData);
            return response;
        }

        [HttpGet]
        [Route("getJobHistory")]
        public async Task<IActionResult> GetJobHistory(string department , int jobId)
        {
            IActionResult response = Unauthorized();
            ListReturnResult<TrackJobDto> trackJob = await _common.TrackJob(department,jobId);
            response = Ok(trackJob);
            return response;
        }

        [HttpGet]
        [Route("getEmployeeForDepartment")]
        public async Task<IActionResult> getEmployeeForDepartment(int departmentId)
        {
            IActionResult response = Unauthorized();
            ListReturnResult<EmployeeDto> emp = await _common.GetEmployeeFromDepartment(departmentId);
            response = Ok(emp);
            return response;
        }
    }
}