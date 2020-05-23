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
    public class DeliveryScreenController : Controller
    {
        private readonly IDeliveryScreen _Del;
        public DeliveryScreenController(IDeliveryScreen Del)
        {
            _Del = Del;
        }

        [HttpPost]
        [Route("AddDeliveryScreen")]
        public async Task<IActionResult> AddDeliveryScreen([FromBody]DeliveryScreenDto del)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<string> delScreen = await _Del.AddDeliveryScreen(del);
            response = Ok(delScreen);
            return response;
        }

        [HttpGet]
        [Route("JobForDeliveryScreen")]
        public async Task<IActionResult> JobForDeliveryScreen()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<AssignedJobDTO> DelScreen = await _Del.GetDeliveryAssignedJob();
            response = Ok(DelScreen);
            return response;
        }

    }
}