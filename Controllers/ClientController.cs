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
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetOneClient(int id) 
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
    }
}
