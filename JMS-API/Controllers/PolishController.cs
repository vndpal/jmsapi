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
        public async Task<IActionResult> AddPolish([FromBody]List<PolishDepartmentDto> polish)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newpolish = await _polish.AddPolish(polish);
            response = Ok(newpolish);
            return response;
        }

        [HttpGet]
        [Route("PolishDetails")]
        public async Task<IActionResult> PolishDetails()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<PolishDepartmentDto> polishDetails = await _polish.GetAllPolish();
            response = Ok(polishDetails);
            return response;
        }

        [HttpGet("{id}")]
        [Route("PolishDetail")]
        public async Task<IActionResult> PolishDetail(int id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<PolishDepartmentDto> polishDetail = await _polish.GetPolish(id);
            response = Ok(polishDetail);
            return response;
        }
    }
}