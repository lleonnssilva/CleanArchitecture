﻿using CleanArchitecture.Application.DTOS.Cliente;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllsync()
        {
            var clientes = await _clienteService.GetAllsync();
            return Ok(clientes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ClienteDTO cliente)
        {

            cliente.Id = Guid.NewGuid();
            if (cliente == null)
                return BadRequest();

            await _clienteService.AddAsync(cliente);

            return Created("Sucesso", null);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _clienteService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, ClienteDTO cliente)
        {

            if (id == Guid.Empty || cliente == null || id != cliente.Id)
                return BadRequest();

            await _clienteService.UpdateAsync(cliente);
            return NoContent();

        }
    }
}
