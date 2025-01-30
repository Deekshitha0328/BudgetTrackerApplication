using BudgetTracker.Services.DTOs;
using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.CreateUser(userDTO);
            if (result)
            {
                return Ok("User registered successfully.");
            }

            return BadRequest("Registration failed. User might already exist.");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _userService.LoginUser(loginDTO);
            return Ok(token);
        }

        [HttpGet("GetAllUser")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, [FromBody] UpdateDTO updatedUserDTO)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID.");
            }
            var updatedUser = await _userService.UpdateUser(id, updatedUserDTO);
            if (updatedUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(updatedUser);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var success = await _userService.DeleteUser(id);
            return success;
        }

    }
}
