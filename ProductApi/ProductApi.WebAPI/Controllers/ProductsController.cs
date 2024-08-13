using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.Services;
using ProductApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace ProductApi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
             _logger = logger;
        }

        [HttpGet("healthZ")]
        [Authorize]
        public async Task<ActionResult<string>> healthZ()
        {
            var product = await _productService.GethealthZ();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{opc}")]
        public async Task<ActionResult<Product>> GetProduct(string opc)
        {
            var product = await _productService.GetProductByIdAsync(opc);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("Product")]
        [Authorize]
        public async Task<ActionResult<Product>> GetProductbyopc(string opc)
        {
            var product = await _productService.GetProductByIdAsync(opc);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("Products")]
        [Authorize]
        public async Task<ActionResult<Product>> GetProductbyopcclo(string? opc, string? clo, int startposition, int pagesize)
        {
            var product = await _productService.GetProductbyopcclo(opc, clo, startposition, pagesize);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { opc = product.OPC }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string opc, Product product)
        {
            if (opc != product.OPC)
            {
                return BadRequest();
            }

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string opc)
        {
            await _productService.DeleteProductAsync(opc);
            return NoContent();
        }
        
    }
}
