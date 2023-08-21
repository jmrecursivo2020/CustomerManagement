using BusinessLogicLayerCustomerManagement;
using DataLayerCustomerManagement;
using DataLayerCustomerManagement.DTOs;
using DataLayerCustomerManagement.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestAPICustomerManagement
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly ClientService _service;

        public ClientServiceTests()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _service = new ClientService(_clientRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateClient_ReturnsFailureResult_WhenEmailExists()
        {
            var dto = new CreateClientDto { Email = "test@test.com" };
            _clientRepositoryMock.Setup(cr => cr.EmailExistsAsync(dto.Email))
                .ReturnsAsync(true);

            var result = await _service.CreateClientAsync(dto);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task DeleteClient_ReturnsNull_WhenIdDoesNotExist()
        {
            var id = 1;
            _clientRepositoryMock.Setup(cr => cr.DeleteClientAsync(id))
                .ReturnsAsync(false);

            var result = await _service.DeleteClientAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetClient_ReturnsNull_WhenIdDoesNotExist()
        {
            var id = 1;
            _clientRepositoryMock.Setup(cr => cr.GetClientAsync(id))
                .ReturnsAsync((Client)null);

            var result = await _service.GetClientAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetClients_ReturnsEmptyList_WhenNoClientsExist()
        {
            _clientRepositoryMock.Setup(cr => cr.GetClientsAsync())
                .ReturnsAsync(new List<Client>());

            var result = await _service.GetClientsAsync();

            Assert.Empty(result);
        }
    }
}
