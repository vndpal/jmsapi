using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [Route("EmployeeRegister")]
        public async Task<IActionResult> EmployeeRegister([FromBody]EmployeeDto empDetails)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newEmployee = await _emp.AddUpdateEmployee(empDetails);
            response = Ok(newEmployee);
            return response;
        }

    }
}
