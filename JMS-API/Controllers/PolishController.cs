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
    public class PolishController : Controller
    {
        private readonly IPolishDepartment _polish;
        public PolishController(IPolishDepartment polish)
        {
            _polish = polish;
        }

        [HttpPost]
        [Route("AddPolish")]
        public async Task<IActionResult> AddPolish([FromBody]PolishDepartmentDto polish)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newpolish = await _polish.AddPolish(polish);
            response = Ok(newpolish);
            return response;
        }

        [HttpPost]
        [Route("UpdatePolish")]
        public async Task<IActionResult> UpdatePolish([FromBody]PolishDepartmentDto polish)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newpolish = await _polish.UpdatePolish(polish);
            response = Ok(newpolish);
            return response;
        }

        [HttpGet]
        [Route("PolishDetails")]
        public async Task<IActionResult> PolishDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<PolishDepartmentDto> polishDetails = await _polish.GetPolishJobs();
            response = Ok(polishDetails);
            return response;
        }

        [HttpGet]
        [Route("PolishJobById")]
        public async Task<IActionResult> GetPolishJobWithId(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<PolishDepartmentDto> polishDetail = await _polish.GetPolishJobWithId(id);
            response = Ok(polishDetail);
            return response;
        }


        [HttpGet]
        [Route("JobForPolish")]
        public async Task<IActionResult> JobForPolish()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<AssignedJobDTO> polishDetails = await _polish.GetPolishAssignedJob();
            response = Ok(polishDetails);
            return response;
        }

        [HttpGet]
        [Route("GetPolishReport")]
        public async Task<IActionResult> GetPolishReport(int jobId, int employeeId, string fromDate, string toDate, int status)
        {
            IActionResult response = Unauthorized();
            ListReturnResult<PolishReportDto> result = await _polish.GetPolishReport(jobId, employeeId, fromDate, toDate, status);
            response = Ok(result);
            return response;
        }
    }
}