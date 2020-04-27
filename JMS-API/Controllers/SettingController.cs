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
    public class SettingController : Controller
    {
        private readonly ISettingDepartment _set;
        public SettingController(ISettingDepartment set)
        {
            _set = set;
        }

        [HttpPost]
        [Route("AddSettingDiamond")]
        public async Task<IActionResult> AddSetting([FromBody]SettingDepartmentDto set)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newset = await _set.AddSetting(set);
            response = Ok(newset);
            return response;
        }

        [HttpPost]
        [Route("UpdateSettingDiamond")]
        public async Task<IActionResult> UpdateSetting([FromBody]SettingDepartmentDto set)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newset = await _set.UpdateSetting(set);
            response = Ok(newset);
            return response;
        }

        [HttpGet]
        [Route("SettingDetails")]
        public async Task<IActionResult> SettingDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<SettingDepartmentDto> setDetails = await _set.GetSettingJobs();
            response = Ok(setDetails);
            return response;
        }

        [HttpGet]
        [Route("SettingJobById")]
        public  IActionResult GetSettingJobWithId(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<SettingDepartmentDto> setDetail = _set.GetSettingJobWithId(id);
            response = Ok(setDetail);
            return response;
        }

        [HttpGet]
        [Route("JobForSetting")]
        public async Task<IActionResult> JobForSetting()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<AssignedJobDTO> setDetails = await _set.GetSettingAssignedJob();
            response = Ok(setDetails);
            return response;
        }


        [HttpGet]
        [Route("StoneByJobId")]
        public async Task<IActionResult> GetStoneByJobId(int id)
        {
            IActionResult response = Unauthorized();
            ListReturnResult<DiamondDetailDto> setDetail =await _set.GetStoneForFitter(id);
            response = Ok(setDetail);
            return response;
        }

    }
}