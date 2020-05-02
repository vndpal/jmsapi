using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using BLL.Repository;
using DTO.DTOModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRController : ControllerBase
    {
        private readonly IHRDepartment _hr;
        public HRController(IHRDepartment hr)
        {
            _hr = hr;
        }

        [HttpPost]
        [Route("AddHRJobs")]
        public async Task<IActionResult> AddHRJobs([FromBody]HRDepartmentDto hrDetails)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newHrDetails = await _hr.AddHR(hrDetails);
            response = Ok(newHrDetails);
            return response;
        }

        [HttpGet]
        [Route("HRDetails")]
        public async Task<IActionResult> HRDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<HRDepartmentDto> hrDetails = await _hr.GetHRDepartmentJobs();
            response = Ok(hrDetails);
            return response;
        }

        [HttpGet]
        [Route("JobForHR")]
        public async Task<IActionResult> JobForHR()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<AssignedJobDTO> hrDetails = await _hr.GetHRAssignedJob();
            response = Ok(hrDetails);
            return response;
        }

        [HttpGet]
        [Route("GetHRReport")]
        public async Task<IActionResult> GetHRReport(int jobId, string fromDate, string toDate)
        {
            IActionResult response = Unauthorized();
            ListReturnResult<HRReportDto> result = await _hr.GetHRReport(jobId, fromDate, toDate);
            response = Ok(result);
            return response;
        }
    }
}