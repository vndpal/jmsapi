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

        [HttpPost]
        [Route("AddTouchUp")]
        public async Task<IActionResult> AddTouchUp([FromBody]TouchUpDto touch)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> newtouch = await _touch.AddTouchUp(touch);
            response = Ok(newtouch);
            return response;
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

        [HttpGet]
        [Route("StoneByJobIdForTouchUp")]
        public async Task<IActionResult> GetStoneByJobIdForTouchUp(int id)
        {
            IActionResult response = Unauthorized();
            ListReturnResult<DiamondDetailDto> setDetail = await _touch.GetStoneForTouchUp(id);
            response = Ok(setDetail);
            return response;
        }

    }
}