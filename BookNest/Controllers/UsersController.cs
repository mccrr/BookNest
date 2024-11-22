using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = "hello world";
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddUser([FromBody] int id)
        {
            if (id == 1) return Ok("Success");
            return BadRequest("Error");
        }
    }
}
