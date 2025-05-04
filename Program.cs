using Microsoft.EntityFrameworkCore;
using SegurOsCar.Models;
using SegurOsCar.Services;
using SegurOsCar.DTOs;
using SegurOsCar.Repository;
using SegurOsCar.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Inyeccion de dependencias
//builder.Services.AddSingleton<>();

// SQL Server connection with Entity Framework
builder.Services.AddDbContext<SecureContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});


builder.Services.AddScoped<VehicleRepository>();

// Services
builder.Services.AddScoped<VehicleRepository>();
builder.Services.AddScoped<VehicleService>();

builder.Services.AddKeyedScoped<ICommonServices<ClientDto, ClientInsertDto, ClientUpdateDto>, ClientService>("clientService");

builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


// deserializador de json
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new VehicleJsonDeserializer());
    });


// Repository
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
//builder.Services.AddScoped<IRepository<Vehicle>, VehicleRepository>();

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    // Configure the HTTP request pipeline.
