using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Services.Dispatchers;
using MyShop.Services.Products;
using MyShop.Services.Products.Commands;
using MyShop.Services.Products.Dtos;
using MyShop.Services.Products.Queries;
using MyShop.Infrastructure.Mvc;
using MyShop.Core.Domain;

namespace MyShop.Api.Controllers
{
    public class ProductsController : ApiController
    {
        public ProductsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseProducts query)
        {
            var products = await _dispatcher.QueryAsync(query);

            return Collection(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> Get([FromRoute] GetProduct query)
        {
            var product = await _dispatcher.QueryAsync(query);
            
            return Single(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProduct command)
        {
            await _dispatcher.SendAsync(command.BindId(c => c.Id));

            return CreatedAtAction(nameof(Get), new GetProduct(){ Id = command.Id }, null);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProduct command)
        // public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _dispatcher.SendAsync(command);

            return NoContent();
        }
    }
}
