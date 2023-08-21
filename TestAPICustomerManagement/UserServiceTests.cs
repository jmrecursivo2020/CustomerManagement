using BusinessLogicLayerCustomerManagement;
using DataLayerCustomerManagement;
using DataLayerCustomerManagement.DTOs;
using Moq;
namespace TestAPICustomerManagement
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _service = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsFailure_WhenUsernameAlreadyExists()
        {
            var username = "testUser";
            _userRepositoryMock.Setup(x => x.GetUserByUsernameAsync(username))
                .ReturnsAsync(new DataLayerCustomerManagement.Entities.User());

            var result = await _service.CreateUserAsync(new UserDto { Username = username });

            Assert.False(result.Success);
        }

        [Fact]
        public async Task LoginAsync_ReturnsFailure_WhenUserDoesNotExist()
        {
            var username = "testUser";
            _userRepositoryMock.Setup(x => x.GetUserByUsernameAsync(username))
                .ReturnsAsync((DataLayerCustomerManagement.Entities.User)null);

            var result = await _service.LoginAsync(new UserDto { Username = username });

            Assert.False(result.Success);
        }

        [Fact]
        public async Task LoginAsync_ReturnsFailure_WhenUserExistsButPasswordsDoNotMatch()
        {
            var username = "testUser";
            _userRepositoryMock.Setup(x => x.GetUserByUsernameAsync(username))
                .ReturnsAsync(new DataLayerCustomerManagement.Entities.User { Password = "wrongPassword" });

            var result = await _service.LoginAsync(new UserDto { Username = username, Password = "testPassword" });

            Assert.False(result.Success);
        }
    }
}