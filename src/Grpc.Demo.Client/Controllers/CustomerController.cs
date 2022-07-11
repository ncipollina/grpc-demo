using Grpc.Demo.Server.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace Grpc.Demo.Client.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{

    private readonly ILogger<CustomerController> _logger;
    private readonly Customer.CustomerClient _customerClient;

    public CustomerController(ILogger<CustomerController> logger, Customer.CustomerClient customerClient)
    {
        _logger = logger;
        _customerClient = customerClient;
    }

    [HttpGet]
    [Route("{id}")]
    
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var customerResponse = await _customerClient.GetCustomerByIdAsync(new GetCustomerByIdRequest
        {
            Id = id
        }, cancellationToken: cancellationToken);
        return Ok(customerResponse.Customer);
    }

    [HttpGet]
    [Route("email/{emailAddress}")]
    public async Task<IActionResult> Get(string emailAddress, CancellationToken cancellationToken)
    {
        var customerResponse = await _customerClient.GetCustomerByEmailAsync(new GetCustomerByEmailRequest
        {
            EmailAddress = emailAddress
        }, cancellationToken: cancellationToken);
        return Ok(customerResponse.Customer);
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var response =
            await _customerClient.GetAllCustomersAsync(new GetAllCustomersRequest(),
                cancellationToken: cancellationToken);
        return Ok(response.Customers);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CustomerRecord customer, CancellationToken cancellationToken)
    {
        var response = await _customerClient.AddCustomerAsync(new AddCustomerRequest
        {
            Customer = customer
        }, cancellationToken: cancellationToken);
        return Ok(response.Customer);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(int id, CustomerRecord customer, CancellationToken cancellationToken)
    {
        var response = await _customerClient.UpdateCustomerAsync(new UpdateCustomerRequest
        {
            Id = id,
            Customer = customer
        }, cancellationToken: cancellationToken);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var response = await _customerClient.DeleteCustomerAsync(new DeleteCustomerRequest
        {
            Id = id
        }, cancellationToken: cancellationToken);
        return NoContent();
    }

}
