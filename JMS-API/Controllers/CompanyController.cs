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
        [Route("AddCompany")]
        public async Task<IActionResult> CompanyRegistration([FromBody]CompanyMasterDto companyDetails)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newCompanyDetails = await _comp.AddCompany(companyDetails);
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
      //  [Route("CompanyDetailsById")]
        public async Task<IActionResult> CompanyDetail(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<CompanyMasterDto> companyDetail = await _comp.GetCompany(id);
            response = Ok(companyDetail);
            return response;
        }

        [HttpPost]
        [Route("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany([FromBody]CompanyMasterDto companyDetails)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newCompanyDetails = await _comp.UpdateCompany(companyDetails);
            response = Ok(newCompanyDetails);
            return response;
        }
    }
}