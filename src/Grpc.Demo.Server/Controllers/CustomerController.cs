using Grpc.Demo.Server.Models;
using Grpc.Demo.Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Grpc.Demo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomer(id, cancellationToken);

        if (customer is not null)
        {
            return Ok(customer);
        }

        return NotFound();
    }

    [HttpGet]
    [Route("email/{emailAddress}")]
    public async Task<IActionResult> Get(string emailAddress, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByEmail(emailAddress, cancellationToken);

        if (customer is not null)
        {
            return Ok(customer);
        }

        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetCustomers(cancellationToken);
        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Customer customer, CancellationToken cancellationToken)
    {
        await _customerRepository.AddCustomer(customer, cancellationToken);
        return Created("", customer);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(int id, Customer customer, CancellationToken cancellationToken)
    {
        await _customerRepository.UpdateCustomer(id, customer, cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _customerRepository.DeleteCustomer(id, cancellationToken);
        return NoContent();
    }
}
