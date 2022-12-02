using System.Net;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.WebApi.Requests;
using FoodRocket.Services.Inventory.Application;
using FoodRocket.Services.Inventory.Application.Commands;
using FoodRocket.Services.Inventory.Application.Commands.Inventory;
using FoodRocket.Services.Inventory.Application.DTO;
using FoodRocket.Services.Inventory.Application.DTO.Inventory;
using FoodRocket.Services.Inventory.Application.Queries;
using FoodRocket.Services.Inventory.Application.Queries.Inventory;
using FoodRocket.Services.Inventory.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodRocket.Services.Inventory.Api.Controllers;
[Route("api/inventory")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Authorize]
public class InventoryController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IAppContext _appContext;
    private readonly INewIdGenerator _idGenerator;

    public InventoryController(IQueryDispatcher queryDispatcher,
         ICommandDispatcher commandDispatcher, IAppContext appContext, INewIdGenerator idGenerator)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _appContext = appContext;
        _idGenerator = idGenerator;
    }

    [HttpPost("products")]
    public async Task<IActionResult> AddProduct([FromBody] AddProduct command)
    {
        command.ProductId = _idGenerator.GetNewIdFor("product");
        await _commandDispatcher.SendAsync(command);
        return Created($"api/inventory/{command.ProductId}", null);
    }

    [HttpDelete("{ProductId}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProduct command)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
    
    [HttpPut("products/add")]
    public async Task<IActionResult> AddProductQuantity([FromBody] AddProductQuantity command)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPut("products/subtract")]
    public async Task<IActionResult> RemoveProductQuantity([FromBody] RemoveProductQuantity command)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPost("units")]
    public async Task<IActionResult> AddUnitOfMeasure([FromBody] AddUnitOfMeasure command)
    {
        await _commandDispatcher.SendAsync(command);
        //TODO: will think about having id of new created entity
        return Created("units/", null);
    }
    
    [HttpPost("storages")]
    public async Task<IActionResult> AddStorage([FromBody] AddStorage command)
    {
        await _commandDispatcher.SendAsync(command);
        return Created("storages/", null);
    }
    
    [HttpPut("storages")]
    public async Task<IActionResult> UpdateStorage([FromBody] UpdateStorage command)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpGet("{ProductId}")]
    public async Task<IActionResult> GetProduct([FromRoute] GetProduct query)
    {
        var product = await _queryDispatcher.QueryAsync(query);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    
    [HttpGet("products/detailed/paginated")]
    public async Task<IActionResult> GetProductDetailedPaginatedList([FromQuery] GetProductDetailsListPaginated query)
    {
        var productsDetailed = await _queryDispatcher.QueryAsync(query);
        return Ok(productsDetailed);
    }
    


    [HttpGet("products/paginated")]
    public async Task<IActionResult> GetProductsPaginatedList([FromQuery] GetProductsListPaginated query)
    {
        var products = await _queryDispatcher.QueryAsync(query);
        return Ok(products);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts([FromQuery] GetProducts query)
    {
        var products = await _queryDispatcher.QueryAsync(query);
        return Ok(products);
    }
    
    [HttpGet("products/{productId}/availability")]
    public async Task<IActionResult> GetProductAvailability([FromRoute] GetProductAvailability query)
    {
        var products = await _queryDispatcher.QueryAsync(query);
        return Ok(products);
    }

    [HttpGet("units")]
    public async Task<IActionResult> GetUnitsOfMeasure()
    {
        var query = new GetUnitsOfMeasure();
        var products = await _queryDispatcher.QueryAsync(query);
        return Ok(products);
    }
}
