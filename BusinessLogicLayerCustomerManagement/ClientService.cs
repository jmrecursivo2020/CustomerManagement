using DataLayerCustomerManagement.DTOs;
using DataLayerCustomerManagement.Entities;
using DataLayerCustomerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayerCustomerManagement
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public async Task<Result<ClientDto>> CreateClientAsync(CreateClientDto createClientDto)
        {
            // Check if email already exists
            if (await _clientRepository.EmailExistsAsync(createClientDto.Email))
            {
                return new Result<ClientDto>
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            var clientEntity = new Client
            {
                Name = createClientDto.Name,
                LastName = createClientDto.LastName,
                Email = createClientDto.Email
            };

            var createdClientEntity = await _clientRepository.CreateClientAsync(clientEntity);

            var createdClientDto = new ClientDto
            {
                Name = createdClientEntity.Name ?? string.Empty,
                LastName = createdClientEntity.LastName ?? string.Empty,
                Email = createdClientEntity.Email ?? string.Empty,
                ClientId = createdClientEntity.ClientId,
            };

            return new Result<ClientDto>
            {
                Success = true,
                Data = createdClientDto
            };
        }

        public async Task<ClientDto> DeleteClientAsync(int id)
        {
            var deleted = await _clientRepository.DeleteClientAsync(id);
            if (!deleted)
            {
                return null;
            }

            return new ClientDto() { ClientId = id };
        }

        public async Task<ClientDto> GetClientAsync(int id)
        {
            var clientEntity = await _clientRepository.GetClientAsync(id);
            
            if (clientEntity == null)
            {
                return null;
            }
            
            var clientDto = new ClientDto
            {
                Name = clientEntity.Name ?? string.Empty,
                LastName = clientEntity.LastName ?? string.Empty,
                Email = clientEntity.Email ?? string.Empty,
                ClientId = clientEntity.ClientId,
            };

            return clientDto;
        }

        public async Task<IEnumerable<ClientDto>> GetClientsAsync()
        {
            var clientEntities = await _clientRepository.GetClientsAsync();

            var clientDtos = clientEntities.Select(entity => new ClientDto
            {
                Name = entity.Name ?? string.Empty,
                LastName = entity.LastName ?? string.Empty,
                Email = entity.Email ?? string.Empty,
                ClientId = entity.ClientId,
            });

            return clientDtos;
        }

        public async Task<ClientDto> UpdateClientAsync(ClientDto clientDto)
        {
            var clientEntity = new Client
            {
                Name = clientDto.Name,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                ClientId = clientDto.ClientId
            };

            var updatedClientEntity = await _clientRepository.UpdateClientAsync(clientEntity);

            var updatedClientDto = new ClientDto
            {
                Name = updatedClientEntity.Name ?? string.Empty,
                LastName = updatedClientEntity.LastName ?? string.Empty,
                Email = updatedClientEntity.Email ?? string.Empty,
                ClientId = updatedClientEntity.ClientId,
            };

            return updatedClientDto;
        }
    }
}
