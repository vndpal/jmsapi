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
        public async Task<IActionResult> AddSetting([FromBody]List<SettingDepartmentDto> set)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newset = await _set.AddSetting(set);
            response = Ok(newset);
            return response;
        }

        [HttpGet]
        [Route("SettingDetails")]
        public async Task<IActionResult> SettingDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<SettingDepartmentDto> setDetails = await _set.GetAllSetting();
            response = Ok(setDetails);
            return response;
        }

        [HttpGet]
        [Route("JobForSetting")]
        public  IActionResult JobForSetting()
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<JobMasterDto> setDetails =  _set.GetSettingAssignedJob();
            response = Ok(setDetails);
            return response;
        }

    }
}