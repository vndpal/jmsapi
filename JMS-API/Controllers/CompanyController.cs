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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyMaster _comp;
        public CompanyController(ICompanyMaster comp)
        {
            _comp = comp;
        }

        // public ICompanyMaster _Comp { get; }

        [HttpPost]
        [Route("CompanyRegister")]
        public async Task<IActionResult> CompanyRegistration([FromBody]List<CompanyMasterDto> companyDetails)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newCompanyDetails = await _comp.AddUpdateCompany(companyDetails);
            response = Ok(newCompanyDetails);
            return response;
        }

        [HttpGet]
        [Route("CompanyDetails")]
        public async Task<IActionResult> CompanyDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<CompanyMasterDto> companyDetails = await _comp.GetAllCompany();
            response = Ok(companyDetails);
            return response;
        }

        [HttpGet("{id}")]
       // [Route("CompanyDetails")]
        public async Task<IActionResult> CompanyDetail(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<CompanyMasterDto> companyDetail = await _comp.GetCompany(id);
            response = Ok(companyDetail);
            return response;
        }

        [HttpPost]
        [Route("updateDetails")]
        public async Task<IActionResult> updateDetails([FromBody] CompanyMasterDto companyDetails)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<CompanyMasterDto> companyDetail = await _comp.updateCompany(companyDetails);
            response = Ok(companyDetail);
            return response;
        }
    }
}