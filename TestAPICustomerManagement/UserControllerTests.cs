using APICustomerManagement.Controllers;
using BusinessLogicLayerCustomerManagement;
using DataLayerCustomerManagement.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace TestAPICustomerManagement
{


    public class UsersControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _controller = new UsersController(_userServiceMock.Object);
    }

    [Fact]
    public async Task CreateUser_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("Username", "Required");
        var result = await _controller.CreateUser(new UserDto());

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("Username", "Required");
        var result = await _controller.Login(new UserDto());

        Assert.IsType<BadRequestObjectResult>(result);
    }


        [Fact]
        public async Task CreateUser_ReturnsBadRequest_WhenUsernameExists()
        {
            _userServiceMock.Setup(x => x.CreateUserAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(new Result { Success = false, Message = "Username already exists." });

            var result = await _controller.CreateUser(new UserDto());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_ReturnsOk_WhenUserIsCreatedSuccessfully()
        {
            _userServiceMock.Setup(x => x.CreateUserAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(new Result { Success = true });

            var result = await _controller.CreateUser(new UserDto());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsBadRequest_WhenLoginFails()
        {
            _userServiceMock.Setup(x => x.LoginAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(new Result { Success = false });

            var result = await _controller.Login(new UserDto());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsOk_WhenLoginIsSuccessful()
        {
            _userServiceMock.Setup(x => x.LoginAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(new Result { Success = true });

            var result = await _controller.Login(new UserDto());

            Assert.IsType<OkObjectResult>(result);
        }
    }
}