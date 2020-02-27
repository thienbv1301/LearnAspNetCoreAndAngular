using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.LoggerService;
using Web.Service.DtoModels;
using Web.Service.UserServices;

namespace WebAPI.Controllers
{
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
    }
}