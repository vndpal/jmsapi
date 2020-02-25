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
    public class CASTController : Controller
    {
        private readonly ICASTDepartment _cast;
        public CASTController(ICASTDepartment cast)
        {
            _cast = cast;
        }

        [HttpPost]
        [Route("AddCAST")]
        public async Task<IActionResult> AddCAST([FromBody]List<CASTDepartmentDto> cast)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newcast = await _cast.AddCAST(cast);
            response = Ok(newcast);
            return response;
        }

        [HttpGet]
        [Route("CASTDetails")]
        public async Task<IActionResult> CASTDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<CASTDepartmentDto> castDetails = await _cast.GetAllCAST();
            response = Ok(castDetails);
            return response;
        }

        [HttpGet("{id}")]
        [Route("CASTDetail")]
        public async Task<IActionResult> CASTDetail(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<CASTDepartmentDto> castDetails = await _cast.GetCAST(id);
            response = Ok(castDetails);
            return response;
        }

    }
}