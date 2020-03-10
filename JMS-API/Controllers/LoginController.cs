using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DTO.DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginMaster _login;
        
        public LoginController(ILoginMaster login)
        {
            _login = login;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginDto login)
        {
            IActionResult Response = Unauthorized();

            SingleReturnResult<string> loginDetails = await _login.AuthenticateUser(login);
            Response = Ok(loginDetails);

            return Response;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]LoginDto userDetails)
        {
            IActionResult Response = Unauthorized("Invalied User");

            SingleReturnResult<LoginDto> registerDetails = await _login.RegisterUser(userDetails);
            Response = Ok(registerDetails);
            return Response;
        }



    }
}