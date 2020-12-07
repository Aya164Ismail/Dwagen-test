using Dwagen.DTO;
using Dwagen.DTO.Users;
using Dwagen.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// add new user in the system
        /// </summary>
        /// <param name="addUserDto"></param>
        /// <returns></returns>
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromForm] AddUserDto addUserDto)
        {
            try
            {
                _logger.LogInformation("Adding New User");
                var result = await _userService.AddUserAsnc(addUserDto);
                if (result.IsCreatedSuccessfully)
                {
                    return Ok(result);
                }
                _logger.LogError(result.ErrorMessages.FirstOrDefault());
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Login user in the system
        /// </summary>
        /// <param name="loginUserDto"></param>
        /// <returns></returns>
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                _logger.LogInformation("Login user");
                var result = await _userService.LoginUser(loginUserDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all users in the system
        /// </summary>
        /// <param name="loginUserDto"></param>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(string include = null)
        {
            try
            {
                _logger.LogInformation("Get all users");
                var result = await _userService.GetAllUsers(include);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get user from the system by Id
        /// </summary>
        /// <param name="loginUserDto"></param>
        /// <returns></returns>
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                _logger.LogInformation("Get user by Id");
                var result = await _userService.GetUserById(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
