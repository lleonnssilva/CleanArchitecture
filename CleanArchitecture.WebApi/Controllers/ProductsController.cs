using CleanArchitecture.Application.DTOS;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllsync()
        {
            var products = await _productService.GetAllsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product != null ? Ok(product) : NotFound("produto não encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductDto product)
        {
            await _productService.AddAsync(product);
            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}
