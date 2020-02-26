using Microsoft.AspNetCore.Mvc;
using Web.Service.UserServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUserByName([FromQuery] string name)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            return Ok(_userService.GetUserByName(name));
        }
    }
}