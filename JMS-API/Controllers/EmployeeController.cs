using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeMaster _emp;
        public EmployeeController(IEmployeeMaster emp)
        {
            _emp = emp;
        }

        // public ICompanyMaster _Comp { get; }

        //[HttpPost]
        //[Route("EmployeeRegister")]
        //public async Task<IActionResult> EmployeeRegister([FromBody]EmployeeDto empDetails)
        //{
        //    IActionResult response = Unauthorized();
        //    SingleReturnResult<string> newEmployee = await _emp.AddUpdateEmployee(empDetails);
        //    response = Ok(newEmployee);
        //    return response;
        //}

        [HttpPost]
        [Route("EmployeeRegister")]
        public async Task<IActionResult> EmployeeRegister()
        {
            try
            {
                var postvalues = HttpContext.Request.Form;

                if (HttpContext.Request.Form.Files.Count() == 0)
                {
                    return BadRequest("No files Found");
                }
                Dictionary<object, object> FormDataKeyValue = new Dictionary<object, object>();
                foreach (var s in postvalues)
                {
                    FormDataKeyValue.Add(s.Key.ToString(), postvalues[s.Key].ToString());
                }

                var formDataJSON = JsonConvert.SerializeObject(FormDataKeyValue);

                var empDetails = JsonConvert.DeserializeObject<EmployeeDto>(formDataJSON);

                var files = HttpContext.Request.Form.Files;


                IActionResult response = Unauthorized();

                SingleReturnResult<string> newEmployee = await _emp.AddUpdateEmployee(empDetails, files);
                response = Ok(newEmployee);
                return response;
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [HttpGet]
        [Route("EmployeeDetails")]
        public async Task<IActionResult> EmployeeDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<EmployeeDto> employeeDetails = await _emp.GetAllEmployee();
            response = Ok(employeeDetails);
            return response;
        }

        [HttpGet]
        [Route("EmployeeDetail")]
        public async Task<IActionResult> EmployeeDetail(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<EmployeeDto> employeeDetail = await _emp.GetEmployee(id);
            response = Ok(employeeDetail);
            return response;
        }

        [HttpGet]
        [Route("getFiles")]
        public async Task<IActionResult> getimages(string group,int refId, string type)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<FilesDto> fileDetails = await _emp.getFile(group,refId,type);
            response = Ok(fileDetails);
            //return response;
            if(fileDetails.Flag == ApplicationConstants.successFlag)
            {
                return File(fileDetails.result.filedata, fileDetails.result.contentType);
            }
            else
            {
                return BadRequest("No such file found");
            }
        }

    }
}
