using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Data.EntityModels;
using Web.LoggerService;
using Web.Service.DtoModels;
using Web.Service.UserServices;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILoggerManager _logger;
        public UserController(IUserService userService, ILoggerManager logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("GetByName")]
        [Authorize(Roles = "User")]
        public IActionResult GetUserByName([FromQuery] string name)
        {                   
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            _logger.LogInfo($"Get user with name '{name}' from the storage ");
            return Ok(_userService.GetUserByName(name));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserRegisterModel user)
        {
            if (_userService.AccountIsExist(user.Account))
            {
                return BadRequest("Account is exist");
            }
            _userService.Register(user);
            return Ok();         
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel userLogin)
        {
            if (!_userService.AccountIsExist(userLogin.Account))
            {
                return BadRequest("Account does not exist");
            }
            User user = _userService.Authenticate(userLogin);
            if (user==null)
            {
                return BadRequest("Password is incorrect");
            }
            string token = _userService.CreateToken(user);
            return Ok(token);
        }
    }
}