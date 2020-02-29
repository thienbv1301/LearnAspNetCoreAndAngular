using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.LoggerService;
using Web.Service.DtoModels;
using Web.Service.UserServices;
using WebAPI.Controllers;
using Xunit;

namespace Web.Test.Controllers
{
    public class UserControllerTest
    {
        Mock<IUserService> mockUserService;
        Mock<ILoggerManager> mockLogger;
        UserController userController;

        public UserControllerTest()
        {
            mockUserService = new Mock<IUserService>();
            mockLogger = new Mock<ILoggerManager>();
            userController = new UserController(mockUserService.Object, mockLogger.Object);
            Setup();
        }

        private void Setup()
        {
            mockUserService
                .Setup(s => s.GetUserByName("thien"))
                .Returns(new UserDto
                    {
                        Id = 1,
                        Name = "thien",
                        Account = "thienbv",
                        Role = "User"
                    });
            mockUserService
                .Setup(s => s.GetUserByName("thien1"))
                .Returns((UserDto)null);
            mockUserService
                .Setup(s => s.AccountIsExist("thienbv"))
                .Returns(true);
        }

        [Fact]
        public void GetUserByName_StringNameIsNull_ReturnBadRequest()
        {
            var result = userController.GetUserByName(null);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetUserByName_StringNameIsEmpty_ReturnBadRequest()
        {
            var result = userController.GetUserByName("");
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetUserByName_StringNameIsWhiteSpace_ReturnBadRequest()
        {
            var result = userController.GetUserByName(" ");
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetUserByName_UserDoesNotExist_ReturnNotFoundObjectResult()
        {
            var result = userController.GetUserByName("thien1");
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GetUserByName_UserIsExist_ReturnOkObjectResult()
        {
            var result = userController.GetUserByName("thien");
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetUserByName_UserIsExist_ReturnUserWithSameName()
        {
            var result = userController.GetUserByName("thien") as OkObjectResult;
            Assert.Equal("thien", (result.Value as UserDto).Name);
        }

        [Fact]
        public void Register_AccountNameIsExist_ReturnBadRequestObjectResult()
        {
            var newUser = new UserRegisterModel
            {
                Name = "bui vu thien",
                Account = "thienbv",
                Password = "123456"
            };
            var result = userController.Register(newUser);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Register_UserPassed_ReturnOkResult()
        {
            var newUser = new UserRegisterModel
            {
                Name = "bui vu thien",
                Account = "thienbv1",
                Password = "123456"
            };
            var result = userController.Register(newUser);
            Assert.IsType<OkResult>(result);
        }
    }
}
