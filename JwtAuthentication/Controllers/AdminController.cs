using AutoMapper;
using Business.Services;
using Data.Dto;
using Data.Models;
using JwtAuthentication.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        private IMapper mapper;
        public AdminController(AdminService adminService, IMapper mapper)
        {
            this.mapper = mapper;
            _adminService = adminService;
        }

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "admin")]
        public IActionResult GetAll()
        {
            var user = _adminService.GetAll();
            return Ok(user);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "admin")]
        public IActionResult Get(string id)
        {
            var user = _adminService.GetById(id);
            if (user == null)
            {
                return Forbid("Yetki Sahibi Değilsiniz");
            }
            return Ok(user);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "admin")]
        public IActionResult Post(AdminDto newUser)
        {
            AdminModels user = mapper.Map<AdminModels>(newUser);
            _adminService.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, newUser);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "admin")]
        public IActionResult Put(AdminModels updateUser)
        {
            var user = _adminService.GetById(updateUser.Id);
            if (user == null)
            {
                return Unauthorized("User information or password is incorrect");
            }
            _adminService.Update(updateUser);
            return NoContent();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "admin")]
        public IActionResult Delete(string id)
        {
            var user = _adminService.GetById(id);
            if (user == null)
            {
                return Forbid("\r\nunauthorized");
            }

            _adminService.Delete(id);
            return NoContent();
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _adminService.Login(model);
            if (user == null)
            {
                return Unauthorized("User information or password is incorrect");
            }
            return Ok(new TokenProvider().CreateToken(user));
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] AdminRegisterModel model)
        {
            var newUser = _adminService.GetByEmail(model.Email);
            if (newUser != null)
            {
                return Conflict("This email is already in use");
            }
            var user = _adminService.Register(model);

            return Ok(new { id = user.Id, email = model.Email });
        }
    }
}
