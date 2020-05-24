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
    public class TouchUpController : Controller
    {
        private readonly ITouchUp _touch;
        public TouchUpController(ITouchUp touch)
        {
            _touch = touch;
        }

        [HttpGet]
        [Route("JobForTouchUp")]
        public async Task<IActionResult> JobForTouchUp()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<AssignedJobDTO> setDetails = await _touch.GetTouchUpAssignedJob();
            response = Ok(setDetails);
            return response;
        }

    }
}