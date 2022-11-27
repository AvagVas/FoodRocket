using FoodRocket.DBContext.Models.Customers;
using Contact = FoodRocket.Services.Inventory.Core.Entities.Customers.Contact;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Customers;

internal static class Extensions
{
    public static FoodRocket.Services.Inventory.Core.Entities.Customers.Customer AsEntity(this Customer customerDbModel)
    {
        Address? mainBillingAddress = null;
        if (customerDbModel.MainBillingAddressId > 0)
        {
            mainBillingAddress =
                customerDbModel.Addresses.FirstOrDefault(address =>
                    address.AddressId == customerDbModel.MainBillingAddressId);
        }

        Address? mainShippingAddress = null;
        if (customerDbModel.MainShippingAddressId > 0)
        {
            mainShippingAddress = customerDbModel.Addresses?.FirstOrDefault(address =>
                address.AddressId == customerDbModel.MainShippingAddressId);
        }

        FoodRocket.DBContext.Models.Customers.Contact? primaryContact = null;
        if (customerDbModel.PrimaryContactId > 0)
        {
            primaryContact = customerDbModel.Contacts?.FirstOrDefault(contact =>
                contact.ContactId == customerDbModel.PrimaryContactId);
        }
        else
        {
            primaryContact = customerDbModel.Contacts?.FirstOrDefault(contact =>
                contact.IsPrimary == true);
        }

        var customer = new FoodRocket.Services.Inventory.Core.Entities.Customers.Customer(
            customerDbModel.CustomerId,
            customerDbModel.FirstName,
            customerDbModel.LastName,
            customerDbModel.IsActive,
            mainBillingAddress?.AsEntity(),
            mainShippingAddress?.AsEntity(),
            customerDbModel?.Contacts?.AsEntity()  ?? Enumerable.Empty<Core.Entities.Customers.Contact>(),
            customerDbModel?.Addresses?.AsEntity() ?? Enumerable.Empty<Core.Entities.Customers.Address>(),
            primaryContact?.AsEnity()
            );

        return customer;
    }

    public static FoodRocket.Services.Inventory.Core.Entities.Customers.Address AsEntity(this Address addressDbModel)
    {
        var entity = new Core.Entities.Customers.Address(addressDbModel.AddressId, addressDbModel.AddressLine,
            addressDbModel.Country, addressDbModel.State, addressDbModel.City, addressDbModel.ZipCode,
            addressDbModel.IsActive, addressDbModel.IsDeleted);
        return entity;
    }

    public static List<FoodRocket.Services.Inventory.Core.Entities.Customers.Address> AsEntity(
        this IEnumerable<Address> addressesDbModel)
    {
        List<FoodRocket.Services.Inventory.Core.Entities.Customers.Address> addressesEntity = new();
        foreach (var addressDbModel in addressesDbModel)
        {
            var addressEntity = new Core.Entities.Customers.Address(addressDbModel.AddressId,
                addressDbModel.AddressLine, addressDbModel.Country, addressDbModel.State, addressDbModel.City,
                addressDbModel.ZipCode, addressDbModel.IsActive, addressDbModel.IsDeleted);
            addressesEntity.Add(addressEntity);
        }

        return addressesEntity;
    }

    public static Core.Entities.Customers.Contact AsEnity(this FoodRocket.DBContext.Models.Customers.Contact contactDb)
    {
        Core.Entities.Customers.ContactType contactType =
            Enum.Parse<Core.Entities.Customers.ContactType>(contactDb.ContactType.ToString());

        Core.Entities.Customers.Contact contact = new(
            contactDb.ContactId,
            contactType,
            contactDb.Name,
            contactDb.Value,
            contactDb.IsPrimary
        );

        return contact;
    }

    public static List<Contact> AsEntity(
        this IEnumerable<FoodRocket.DBContext.Models.Customers.Contact> contactsDb)
    {
        List<Contact> contacts = new();
        contacts.AddRange(contactsDb.Select(contactDb => contactDb.AsEnity()));
        return contacts;
    }

    public static Customer AsDbModel(this Core.Entities.Customers.Customer customer)
    {
        var customerDb = new Customer();
        customerDb.CustomerId = customer.Id;
        customerDb.FirstName = customer.FirstName;
        customerDb.LastName = customer.LastName;
        if (customer.PrimaryContact is { })
        {
            customerDb.PrimaryContactId = customer.PrimaryContact.Id;
        }

        if (customer.MainShippingAddress is { })
        {
            customerDb.MainShippingAddressId = customer.MainShippingAddress.Id;
        }

        if (customer.MainBillingAddress is { })
        {
            customerDb.MainBillingAddressId = customer.MainBillingAddress.Id;
        }

        customerDb.IsActive = customer.IsActive;
        customerDb.IsDeleted = customer.IsDeleted;
        customerDb.CreatedAt = customer.CreatedAt;
        
        customerDb.Addresses = customer.Addresses.AsDbModel(customerDb);
        customerDb.Contacts = customer.Contacts.AsDbModel(customerDb);

        return customerDb;
    }

    public static ICollection<Customer> AsDbModel(this IEnumerable<Core.Entities.Customers.Customer> customers)
    {
        List<Customer> customersDb = new();
        customersDb.AddRange(customers.Select(c => c.AsDbModel()));
        return customersDb;
    }

    public static Address AsDbModel(this Core.Entities.Customers.Address address, Customer customer)
    {
        Address addressDb = new();
        addressDb.AddressId = address.Id;
        addressDb.AddressLine = address.AddressLine;
        addressDb.Country = address.Country;
        addressDb.State = address.State;
        addressDb.City = address.City;
        addressDb.ZipCode = address.ZipCode;
        addressDb.IsActive = address.IsActive;
        addressDb.IsDeleted = address.IsDeleted;
        addressDb.Customer = customer;
        return addressDb;
    }

    public static ICollection<Address> AsDbModel(this IEnumerable<Core.Entities.Customers.Address> addresses,
        Customer customer)
    {
        List<Address> customersDb = new();
        customersDb.AddRange(addresses.Select(c => c.AsDbModel(customer)));
        return customersDb;
    }

    public static FoodRocket.DBContext.Models.Customers.Contact AsDbModel(this Core.Entities.Customers.Contact contact,
        Customer customer)
    {
        FoodRocket.DBContext.Models.Customers.Contact contactDb = new();
        contactDb.ContactId = contact.Id;
        contactDb.Name = contact.Name;
        FoodRocket.DBContext.Models.Customers.ContactType contactTypeDb;
        if (ContactType.TryParse(contact.ContactType.ToString(), out contactTypeDb))
        {
            contactDb.ContactType = contactTypeDb;
        }

        contactDb.Value = contact.Value;
        contactDb.IsPrimary = contact.IsPrimary;
        contactDb.Customer = customer;

        return contactDb;
    }

    public static ICollection<FoodRocket.DBContext.Models.Customers.Contact> AsDbModel(
        this IEnumerable<Core.Entities.Customers.Contact> contacts, Customer customer)
    {
        List<FoodRocket.DBContext.Models.Customers.Contact> contactsDb = new();
        contactsDb.AddRange(contacts.Select(c => c.AsDbModel(customer)));
        return contactsDb;
    }

    // internal static int AsDaysSinceEpoch(this DateTime dateTime)
    //     => (dateTime - new DateTime()).Days;
    //     
    // internal static DateTime AsDateTime(this int daysSinceEpoch)
    //     => new DateTime().AddDays(daysSinceEpoch);
}