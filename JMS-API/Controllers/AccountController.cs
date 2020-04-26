using BLL.Interface;
using DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repo;

        public AccountController(IAccountRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserMasterModel userDetails)
        {
            IActionResult Response = Unauthorized("Invalied User");
            //Converting EmailId to Lower case.
            userDetails.EmailId = userDetails.EmailId.ToLower();
            SingleReturnResult<string> registerDetails = await _repo.Register(userDetails);
            Response = Ok(registerDetails);
            return Response;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            IActionResult Response = Unauthorized();

            SingleReturnResult<ResponseDto> loginDetails = await _repo.Login(login);
            Response = Ok(loginDetails);

            return Response;
        }
    }
}
