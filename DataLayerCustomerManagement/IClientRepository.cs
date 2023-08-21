using DataLayerCustomerManagement.Entities;

namespace DataLayerCustomerManagement
{
    public interface IClientRepository
    {
        Task<Client> CreateClientAsync(Client client);
        Task<bool> DeleteClientAsync(int id);
        Task<Client> GetClientAsync(int id);
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> UpdateClientAsync(Client client);
        Task<bool> EmailExistsAsync(string email);
    }
}
