using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs.User;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Models.Tickets;
using HelpDeskAPI.Models.Users;
using HelpDeskAPI.Services.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAllUsers();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserId([FromRoute] int id)
        {
            var user = await _userService.GetUserById(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            var user = await _userService.CreateUser(userDto);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, UpdateUserDto userDto)
        {
            var user = await _userService.UpdateUser(id, userDto);

            return CreatedAtAction(
                 nameof(GetUserId),
                 new { id = user.Id },
                 user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await _userService.DeleteUser(id);

            return NoContent();
        }

    }
}
