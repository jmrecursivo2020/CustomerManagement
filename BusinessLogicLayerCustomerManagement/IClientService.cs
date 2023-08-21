using DataLayerCustomerManagement.DTOs;

namespace BusinessLogicLayerCustomerManagement
{
    public interface IClientService
    {
        Task<Result<ClientDto>> CreateClientAsync(CreateClientDto createClientDto);
        Task<ClientDto> DeleteClientAsync(int id);
        Task<ClientDto> GetClientAsync(int id);
        Task<IEnumerable<ClientDto>> GetClientsAsync();
        Task<ClientDto> UpdateClientAsync(ClientDto clientDto);
    }
}