using Grpc.Demo.Server.Repositories;
using Grpc.Demo.Server.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel().ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(endpointOptions => endpointOptions.Protocols = HttpProtocols.Http2);
});

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<CustomerService>();

app.Run();
