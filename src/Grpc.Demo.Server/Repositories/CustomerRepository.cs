using Bogus;
using Grpc.Demo.Server.Models;

namespace Grpc.Demo.Server.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = new();

    public CustomerRepository()
    {
        Randomizer.Seed = new Random(55);
        var customerFaker = new Faker<Customer>()
            .RuleFor(x => x.Id, faker => faker.IndexFaker)
            .RuleFor(x => x.FullName, faker => faker.Person.FullName)
            .RuleFor(x => x.Email, faker => faker.Person.Email)
            .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth);
        _customers = customerFaker.Generate(10);
    }

    public async Task<Customer?> GetCustomer(int id, CancellationToken cancellationToken = default)
    {
        return _customers.FirstOrDefault(customer => customer.Id == id);
    }

    public async Task<IEnumerable<Customer>> GetCustomers(CancellationToken cancellationToken = default)
    {
        return _customers;
    }

    public async Task<Customer?> GetCustomerByEmail(string emailAddress, CancellationToken cancellationToken = default)
    {
        return _customers.FirstOrDefault(customer => customer.Email.ToLower().Equals(emailAddress.ToLower()));
    }

    public async Task AddCustomer(Customer customer, CancellationToken cancellationToken = default)
    {
        customer.Id = _customers.Count;
        _customers.Add(customer);
    }

    public async Task DeleteCustomer(int id, CancellationToken cancellationToken = default)
    {
        var customer = await GetCustomer(id);
        if (customer is not null)
        {
            _customers.Remove(customer);
        }

    }

    public async Task UpdateCustomer(int id, Customer customer, CancellationToken cancellationToken = default)
    {
        var index = _customers.FindIndex(cust => cust.Id == id);

        if (index != -1)
            _customers[index] = customer;

    }
}