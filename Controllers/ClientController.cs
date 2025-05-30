﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SegurOsCar.Services;
using SegurOsCar.DTOs;
using FluentValidation;

namespace SegurOsCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ICommonServices<ClientDto, ClientInsertDto, ClientUpdateDto> _clientService;
        private readonly IValidator<ClientInsertDto> _clientInsertValidator;
        public ClientController([FromKeyedServices("clientService")] ICommonServices<ClientDto, ClientInsertDto, ClientUpdateDto> clientService, IValidator<ClientInsertDto> clientInsertValidator)
        {
            _clientService = clientService;
            _clientInsertValidator = clientInsertValidator;
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
            var clientValidationResult = await _clientInsertValidator.ValidateAsync(clientInsertDto);
            if(!clientValidationResult.IsValid)
                return BadRequest(clientValidationResult.Errors[0].ErrorMessage);
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
