using FoodRocket.Services.Inventory.Core.Events.Customers;
using FoodRocket.Services.Inventory.Core.Exceptions.Customers;

namespace FoodRocket.Services.Inventory.Core.Entities.Customers;

public class Customer : AggregateRoot
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Contact? PrimaryContact { get; private set; }

    private List<Contact> _contacts = new();

    private List<Address> _addresses = new();

    public Address? MainShippingAddress { get; private set; }

    public Address? MainBillingAddress { get; private set; }

    public IEnumerable<Contact> Contacts
    {
        get => _contacts;
        private set => _contacts = new(value);
    }

    public IEnumerable<Address> Addresses
    {
        get => _addresses;
        private set => _addresses = new(value);
    }


    public static Customer Create(long id, string firstName, string lastName, bool isActive)
    {
        var customer = new Customer(id, firstName, lastName, isActive, null, null, Enumerable.Empty<Contact>(), Enumerable.Empty<Address>(), null);
        customer.AddEvent(new CustomerCreated(customer));
        return customer;
    }

    public Customer(long id, string firstName, string lastName, bool isActive, Address? mainBillingAddress,
        Address? mainShippingAddress, IEnumerable<Contact> contacts, IEnumerable<Address> addresses, Contact? primaryContact)
    {
        ValidateCustomer(firstName, lastName, isActive, mainBillingAddress, mainShippingAddress);
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        IsActive = isActive;
        IsDeleted = false;
        MainBillingAddress = mainBillingAddress;
        MainShippingAddress = mainShippingAddress;
        Addresses = addresses;
        Contacts = contacts;
        CreatedAt = DateTime.Now;
        PrimaryContact = primaryContact;
    }

    public void AddContact(Contact contact)
    {
        if (contact.IsPrimary)
        {
            PrimaryContact = contact;
        }

        _contacts.Add(contact);
        AddEvent(new CustomerUpdated(this));
    }

    public void ChangeShippingAddress(Address address)
    {
        MainShippingAddress = address;
        AddEvent(new ShippingAddressUpdated(this));
    }

    public void ChangeBillingAddress(Address address)
    {
        MainBillingAddress = address;
        AddEvent(new BillingAddressUpdated(this));
    }

    public void SetContactAsPrimary(Contact contact)
    {
        PrimaryContact = contact;
        var prevPrimaryContact = Contacts.FirstOrDefault(contact => contact.IsPrimary);
        if (prevPrimaryContact is {} && prevPrimaryContact.Id != contact.Id)
        {
            prevPrimaryContact.ChangePrimaryStatus(false);
        }
    }
    public void AddAddress(Address address)
    {
        AddAddress(new[] { address });
    }

    public void AddAddress(IEnumerable<Address> addresses)
    {
        _addresses.AddRange(addresses);
        AddEvent(new CustomerUpdated(this));
    }


    public void Delete()
    {
        IsDeleted = true;
        AddEvent(new CustomerDeleted(this));
    }

    private static void ValidateCustomer(string firstName, string lastName, bool isActive, Address mainBillingAddress,
        Address mainShippingAddress)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new InvalidCustomerException("First name should be provided");
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new InvalidCustomerException("Last name should be provided");
        }
    }
}