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

        [HttpGet]
        [Route("GetMenuByRoleId")]
        public async Task<IActionResult> GetMenuByRoleId(int roleId)
        {
            IActionResult Response = Unauthorized();

            ListReturnResult<MenuListDto> menus = await _repo.GetMenuByRoleId(roleId);
            Response = Ok(menus);

            return Response;
         }

        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            IActionResult response = Unauthorized();
            ListReturnResult<UserMasterModel> users = await _repo.GetUsers();
            response = Ok(users);
            return response;
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(long id)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<UserMasterModel> user = await _repo.GetUserById(id);
            response = Ok(user);
            return response;
        }

        [HttpPost]
        [Route("SetRoleMenuMapping")]
        public async Task<IActionResult> SetRoleMenuMapping(RoleMenuModel roleMenu)
        {
            IActionResult response = Unauthorized();
            SingleReturnResult<int> result = await _repo.SetRoleMenuMapping(roleMenu);
            response = Ok(result);
            return response;
        }
    }
}
