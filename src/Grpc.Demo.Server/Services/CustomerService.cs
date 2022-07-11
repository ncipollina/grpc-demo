using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Demo.Server.Grpc;
using Grpc.Demo.Server.Repositories;

namespace Grpc.Demo.Server.Services;

public class CustomerService : Customer.CustomerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public override async Task<GetCustomerByIdResponse> GetCustomerById(GetCustomerByIdRequest request, ServerCallContext context)
    {
        var customer = await _customerRepository.GetCustomer(request.Id, context.CancellationToken);

        if (customer is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Customer not found"));
        }
        return new GetCustomerByIdResponse
        {
            Customer = MapCustomerToCustomerRecord(customer)
        };
    }

    public override async Task<GetCustomerByEmailResponse> GetCustomerByEmail(GetCustomerByEmailRequest request, ServerCallContext context)
    {
        var customer = await _customerRepository.GetCustomerByEmail(request.EmailAddress, context.CancellationToken);
        if (customer is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Customer not found"));
        }
        return new GetCustomerByEmailResponse()
        {
            Customer = MapCustomerToCustomerRecord(customer)
        };
    }

    public override async Task<GetAllCustomersResponse> GetAllCustomers(GetAllCustomersRequest request, ServerCallContext context)
    {
        var customers = await _customerRepository.GetCustomers(context.CancellationToken);

        return new GetAllCustomersResponse
        {
            Customers = { customers.Select(MapCustomerToCustomerRecord) }
        };
    }

    public override async Task<AddCustomerResponse> AddCustomer(AddCustomerRequest request, ServerCallContext context)
    {
        var customer = MapCustomerRecordToCustomer(request.Customer);

        await _customerRepository.AddCustomer(customer, context.CancellationToken);

        return new AddCustomerResponse
        {
            Customer = MapCustomerToCustomerRecord(customer)
        };
    }

    public override async Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest request, ServerCallContext context)
    {
        var customer = MapCustomerRecordToCustomer(request.Customer);

        await _customerRepository.UpdateCustomer(request.Id, customer, context.CancellationToken);

        return new UpdateCustomerResponse();
    }

    public override async Task<DeleteCustomerResponse> DeleteCustomer(DeleteCustomerRequest request, ServerCallContext context)
    {
        await _customerRepository.DeleteCustomer(request.Id, context.CancellationToken);

        return new DeleteCustomerResponse();
    }

    private static Models.Customer MapCustomerRecordToCustomer(CustomerRecord customerRecord)
    {
        return new Models.Customer
        {
            Id = customerRecord.Id,
            FullName = customerRecord.FullName,
            Email = customerRecord.Email,
            DateOfBirth = customerRecord.DateOfBirth.ToDateTime()
        };
    }
    private static CustomerRecord MapCustomerToCustomerRecord(Models.Customer customer)
    {
        return new CustomerRecord
        {
            Id = customer.Id,
            FullName = customer.FullName,
            Email = customer.Email,
            DateOfBirth = customer.DateOfBirth.ToUniversalTime().ToTimestamp()
        };
    }

}