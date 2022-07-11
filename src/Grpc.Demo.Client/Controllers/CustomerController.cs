using Microsoft.AspNetCore.Mvc;

namespace Grpc.Demo.Client.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{

    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("{id}")]
    
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet]
    [Route("email/{emailAddress}")]
    public async Task<IActionResult> Get(string emailAddress, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(Customer customer, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(int id, Customer customer, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return Ok();
    }

}

public class Customer
{
}
