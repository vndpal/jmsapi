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
    }
}