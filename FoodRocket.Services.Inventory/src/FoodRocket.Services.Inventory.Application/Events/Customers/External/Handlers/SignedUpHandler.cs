using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using FoodRocket.Services.Inventory.Application.Exceptions;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Core.Entities.Customers;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using FoodRocket.Services.Inventory.Core.Repositories.Customers;

namespace FoodRocket.Services.Inventory.Application.Events.Customers.External.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<SignedUpHandler> _logger;
        private readonly INewIdGenerator _idGenerator;

        public SignedUpHandler(ICustomerRepository customerRepository,
            ILogger<SignedUpHandler> logger, INewIdGenerator idGenerator)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _idGenerator = idGenerator;
        }

        public async Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = new CancellationToken())
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            if (string.IsNullOrWhiteSpace(@event.FirstName)) throw new ArgumentNullException(nameof(@event.FirstName));
            if (string.IsNullOrWhiteSpace(@event.LastName)) throw new ArgumentNullException(nameof(@event.LastName));

            if (@event.UserType != "customer")
            {
                return;
            }

            Customer customer = Customer.Create(_idGenerator.GetNewIdFor("customer"), @event.FirstName, @event.LastName,
                true);

            await _customerRepository.AddAsync(customer);
        }
    }
}