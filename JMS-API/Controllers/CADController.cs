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
    public class CADController : Controller
    {
        private readonly ICADDepartment _cad;
        public CADController(ICADDepartment cad)
        {
            _cad = cad;
        }

        [HttpPost]
        [Route("AddCAD")]
        public async Task<IActionResult> AddCAD([FromBody]List<CADDepartmentDto> cad)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newcad = await _cad.AddCAD(cad);
            response = Ok(newcad);
            return response;
        }

        [HttpGet]
        [Route("CADDetails")]
        public async Task<IActionResult> CADDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<CADDepartmentDto> cadDetails = await _cad.GetAllCAD();
            response = Ok(cadDetails);
            return response;
        }

        [HttpGet("{id}")]
        [Route("CADDetail")]
        public async Task<IActionResult> CADDetail(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<CADDepartmentDto> cadDetail = await _cad.GetCAD(id);
            response = Ok(cadDetail);
            return response;
        }

        [HttpGet]
        [Route("JobForCAD")]
        public async Task<IActionResult> JobForCAD()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<JobMasterDto> cadDetails = await _cad.GetCADAssignedJob();
            response = Ok(cadDetails);
            return response;
        }

        [HttpPost]
        [Route("updateDetails")]
        public async Task<IActionResult> updateDetails([FromBody] CADDepartmentDto cadDetails)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<CADDepartmentDto> cadDetail = await _cad.UpdateCAD(cadDetails);
            response = Ok(cadDetail);
            return response;
        }
    }
}