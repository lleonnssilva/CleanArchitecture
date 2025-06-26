using CleanArchitecture.Application.DTOS;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllsync()
        {
            var clientes = await _clienteService.GetAllsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            return cliente != null ? Ok(cliente) : NotFound("cliente não encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ClienteDTO cliente)
        {
            await _clienteService.AddAsync(cliente);
            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _clienteService.DeleteAsync(id);
            return  Ok();
        }
    }
}
