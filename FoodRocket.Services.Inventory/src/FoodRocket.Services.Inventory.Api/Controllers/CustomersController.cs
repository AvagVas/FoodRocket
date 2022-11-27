using System.Net;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.WebApi.Requests;
using FoodRocket.Services.Inventory.Application;
using FoodRocket.Services.Inventory.Application.Commands;
using FoodRocket.Services.Inventory.Application.Commands.Customers;
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
[Route("api/customers")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IAppContext _appContext;
    private readonly INewIdGenerator _idGenerator;

    public CustomersController(IQueryDispatcher queryDispatcher,
         ICommandDispatcher commandDispatcher, IAppContext appContext, INewIdGenerator idGenerator)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _appContext = appContext;
        _idGenerator = idGenerator;
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] AddCustomer command)
    {
        if (command.CustomerId == 0)
        {
            command.CustomerId = _idGenerator.GetNewIdFor("customer");
        }

        await _commandDispatcher.SendAsync(command);
        return Created("/customers", null);
    }

    [HttpPut("add_address")]
    public async Task<IActionResult> AddAddressToCustomer([FromBody] AddAddressToCustomer command)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }

    [HttpPut("add_contact")]
    public async Task<IActionResult> AddContactToCustomer([FromBody] AddContactToCustomer command)
    {
        await _commandDispatcher.SendAsync(command);
        return Ok();
    }
}
