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
        public async Task<IActionResult> AddHRJobs([FromBody]List<HRDepartmentDto> hrDetails)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newHrDetails = await _hr.AddUpdateHR(hrDetails);
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
            ListReturnResult<JobMasterDto> hrDetails = await _hr.GetHRAssignedJob();
            response = Ok(hrDetails);
            return response;
        }
    }
}