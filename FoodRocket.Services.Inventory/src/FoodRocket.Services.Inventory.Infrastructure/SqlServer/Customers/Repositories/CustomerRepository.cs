using FoodRocket.DBContext.Contexts;
using FoodRocket.DBContext.Models.Customers;
using FoodRocket.Services.Inventory.Core.Entities;
using FoodRocket.Services.Inventory.Core.Repositories.Customers;
using Microsoft.EntityFrameworkCore;
using Customer = FoodRocket.Services.Inventory.Core.Entities.Customers.Customer;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Customers.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomersDbContext _dbContext;

    public CustomerRepository(CustomersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> GetAsync(AggregateId id)
    {
        var foundCustomer = await _dbContext.Customers
                        .Include(c => c.Addresses)
                        .Include(c => c.Contacts)
                        .FirstOrDefaultAsync(c => c.CustomerId == id);

        if (foundCustomer is null)
        {
            return null;
        }

        var customerEntity = foundCustomer.AsEntity();
        return customerEntity;
    }

    public async Task<bool> ExistsAsync(AggregateId id)
    {
        var foundCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        return foundCustomer is { };
    }


    public async Task AddAsync(Customer customer)
    {
        DBContext.Models.Customers.Customer customerDb = new DBContext.Models.Customers.Customer();
        customerDb.CustomerId = customer.Id;
        customerDb.FirstName = customer.FirstName;
        customerDb.LastName = customer.LastName;
        customerDb.IsActive = customer.IsActive;
        customerDb.CreatedAt = customer.CreatedAt;
        if (customer.Contacts.Any())
        {
            List<Contact> contactDbs = new();
            foreach (var contact in customer.Contacts)
            {
                DBContext.Models.Customers.Contact contactDb = new();
                contactDb.ContactId = contact.Id;
                contactDb.Customer = customerDb;
                contactDb.Name = contact.Name;
                contactDb.IsPrimary = contact.IsPrimary;
                contactDb.Value = contact.Value;
                if (ContactType.TryParse(contact.ContactType.ToString(), out ContactType contactType))
                {
                    contactDb.ContactType = contactType;
                }

                contactDbs.Add(contactDb);
            }

            await _dbContext.AddRangeAsync(contactDbs);
            if (customer.PrimaryContact is { })
            {
                customerDb.PrimaryContactId = customer.PrimaryContact.Id;
            }
        }

        if (customer.Addresses.Any())
        {
            List<Address> addressDbModels = new();
            foreach (var address in customer.Addresses)
            {
                Address addressDb = new();
                addressDb.AddressId = address.Id;
                addressDb.Country = address.Country;
                addressDb.State = address.State;
                addressDb.City = address.City;
                addressDb.AddressLine = address.AddressLine;
                addressDb.Customer = customerDb;

                addressDbModels.Add(addressDb);
            }

            await _dbContext.AddRangeAsync(addressDbModels);

            if (customer.MainBillingAddress is { })
            {
                customerDb.MainBillingAddressId = customer.MainBillingAddress.Id;
            }

            if (customer.MainShippingAddress is { })
            {
                customerDb.MainShippingAddressId = customer.MainShippingAddress.Id;
            }
        }

        _dbContext.Customers.Add(customerDb);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        var customerDb = customer.AsDbModel();

        await _dbContext.SaveChangesAsync();
        var originalCustomer = _dbContext.Customers.FirstOrDefault(someCustomerDb => someCustomerDb.CustomerId == customerDb.CustomerId);
        _dbContext.Entry(originalCustomer!).CurrentValues.SetValues(customerDb);

        if (customerDb.Addresses?.Any() ?? false)
        {
            foreach (var address in customerDb.Addresses)
            {
                var originalAddress =
                    originalCustomer!.Addresses.FirstOrDefault(item => item.AddressId == address.AddressId);
                if (originalAddress is {})
                {
                    _dbContext.Entry(originalAddress).CurrentValues.SetValues(address);
                }
                else
                {
                    _dbContext.Entry(address).State = EntityState.Added;
                }
            }
        } 
        else
        {
            if (originalCustomer!.Addresses?.Any() ?? false)
            {
                foreach (var address in originalCustomer.Addresses)
                {
                    _dbContext.Addresses.Remove(address);
                }
            }
        }

        if (customerDb.Contacts?.Any() ?? false)
        {
            foreach (var contact in customerDb.Contacts)
            {
                var originalContact =
                    originalCustomer!.Contacts.FirstOrDefault(item => item.ContactId == contact.ContactId);
                if (originalContact is {})
                {
                    _dbContext.Entry(originalContact).CurrentValues.SetValues(contact);
                }
                else
                {
                    _dbContext.Entry(contact).State = EntityState.Added;
                }
            }
        } 
        else
        {
            if (originalCustomer!.Contacts?.Any() ?? false)
            {
                foreach (var contact in originalCustomer.Contacts)
                {
                    _dbContext.Contacts.Remove(contact);
                }
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(AggregateId id)
    {
        throw new NotImplementedException();
    }
}