using APICustomerManagement.Controllers;
using BusinessLogicLayerCustomerManagement;
using DataLayerCustomerManagement.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestAPICustomerManagement
{
    public class ClientsControllerTests
    {
        private readonly Mock<IClientService> _clientServiceMock;
        private readonly ClientsController _controller;

        public ClientsControllerTests()
        {
            _clientServiceMock = new Mock<IClientService>();
            _controller = new ClientsController(_clientServiceMock.Object);
        }

        [Fact]
        public async Task GetClients_ReturnsOk_WhenCalled()
        {
            _clientServiceMock.Setup(cs => cs.GetClientsAsync())
                .ReturnsAsync(new List<ClientDto>());

            var result = await _controller.GetClients();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetClient_ReturnsOk_WhenIdExists()
        {
            var id = 1;
            _clientServiceMock.Setup(cs => cs.GetClientAsync(id))
                .ReturnsAsync(new ClientDto { ClientId = id });

            var result = await _controller.GetClient(id);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetClient_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var id = 1;
            _clientServiceMock.Setup(cs => cs.GetClientAsync(id))
                .ReturnsAsync((ClientDto)null);

            var result = await _controller.GetClient(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateClient_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Email", "Required");
            var result = await _controller.CreateClient(new CreateClientDto());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateClient_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Email", "Required");
            var result = await _controller.UpdateClient(new ClientDto());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateClient_ReturnsNotFound_WhenClientDoesNotExist()
        {
            _clientServiceMock.Setup(cs => cs.UpdateClientAsync(It.IsAny<ClientDto>()))
                .ReturnsAsync((ClientDto)null);

            var result = await _controller.UpdateClient(new ClientDto());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteClient_ReturnsNotFound_WhenClientDoesNotExist()
        {
            var id = 1;
            _clientServiceMock.Setup(cs => cs.DeleteClientAsync(id))
                .ReturnsAsync((ClientDto)null);

            var result = await _controller.DeleteClient(id);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
