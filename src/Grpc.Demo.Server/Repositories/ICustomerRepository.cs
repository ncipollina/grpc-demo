using Grpc.Demo.Server.Models;

namespace Grpc.Demo.Server.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomer(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Customer>> GetCustomers(CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerByEmail(string emailAddress, CancellationToken cancellationToken = default);
    Task AddCustomer(Customer customer, CancellationToken cancellationToken = default);
    Task DeleteCustomer(int id, CancellationToken cancellationToken = default);
    Task UpdateCustomer(int id, Customer customer, CancellationToken cancellationToken = default);
}