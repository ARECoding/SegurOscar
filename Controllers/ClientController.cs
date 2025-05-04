using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SegurOsCar.Services;
using SegurOsCar.DTOs;

namespace SegurOsCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ICommonServices<ClientDto, ClientInsertDto, ClientUpdateDto> _clientService;
        public ClientController([FromKeyedServices("clientService")] ICommonServices<ClientDto, ClientInsertDto, ClientUpdateDto> clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetClients()
        {

            var clientDto = await _clientService.Get();
            return Ok(clientDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneClient(string id)
        {
            var clientDto = await _clientService.GetById(id);
            return clientDto == null ? NotFound() : Ok(clientDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddClients(ClientInsertDto clientInsertDto)
        {
            await _clientService.Add(clientInsertDto);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(string id, ClientUpdateDto updatedClient)
        {
            var client = await _clientService.Update(id, updatedClient);
            return client == null ? NotFound() : Ok(client);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            var client = await _clientService.Delete(id);
            return client == null ? NotFound() : NoContent();
        }
    }
}
