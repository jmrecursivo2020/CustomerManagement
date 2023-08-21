using BusinessLogicLayerCustomerManagement;
using DataLayerCustomerManagement.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace APICustomerManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
        {
            var clients = await _clientService.GetClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            var client = await _clientService.GetClientAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientDto createClientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _clientService.CreateClientAsync(createClientDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetClient), new { id = result.Data.ClientId }, result.Data);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedClient = await _clientService.UpdateClientAsync(clientDto);
            if (updatedClient == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            //arreglar esto
            var deletedClient = await _clientService.DeleteClientAsync(id);
            if (deletedClient == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
