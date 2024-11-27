using BookNest.Dtos;
using BookNest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;
        public UsersController(UserService userService) {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var result = await userService.GetUsers(cancellationToken);
            return Ok(result);
        }
        [HttpGet("${id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await userService.GetById(id);
            if (result == null) return NotFound("User not found");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] SignUpDto signUpDto)
        {
            var dbUser = await userService.CreateUser(signUpDto);
            if (dbUser == null) return BadRequest("User couldn't be created");
            return Ok(dbUser);
        }
    }
}
