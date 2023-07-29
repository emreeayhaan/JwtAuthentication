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
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private IMapper mapper;
        public UserController(UserService userService, IMapper mapper)
        {
            this.mapper = mapper;
            _userService = userService;
        }

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "user")]
        public IActionResult GetAll()
        {
            var user = _userService.GetAll();
            return Ok(user);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "user")]
        public IActionResult Get(string id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return Forbid("Yetki Sahibi Değilsiniz");
            }
            return Ok(user);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "user")]
        public IActionResult Post(UserDto newUser)
        {
            UserModels user = mapper.Map<UserModels>(newUser);
            _userService.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, newUser);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "user")]
        public IActionResult Put(UserModels updateUser)
        {
            var user = _userService.GetById(updateUser.Id);
            if (user == null)
            {
                return Unauthorized("User information or password is incorrect");
            }
            _userService.Update(updateUser);
            return NoContent();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "user")]
        public IActionResult Delete(string id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return Forbid("\r\nunauthorized");
            }

            _userService.Delete(id);
            return NoContent();
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _userService.Login(model);
            if (user == null)
            {
                return Unauthorized("User information or password is incorrect");
            }
            return Ok(new TokenProvider().CreateToken(user));
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegisterModel model)
        {
            var newUser = _userService.GetByEmail(model.Email);
            if (newUser != null)
            {
                return Conflict("This email is already in use");
            }
            var user = _userService.Register(model);

            return Ok(new { id = user.Id, email = model.Email });
        }
    }
}
