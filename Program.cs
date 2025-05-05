using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SegurOsCar.AutoMappers;
using SegurOsCar.DTOs;
using SegurOsCar.Models;
using SegurOsCar.Repository;
using SegurOsCar.Services;
using SegurOsCar.Utilities;
using SegurOsCar.Validators;

var builder = WebApplication.CreateBuilder(args);

// Inyeccion de dependencias

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

// Validators

builder.Services.AddScoped<IValidator<ClientInsertDto>, ClientInsertValidator>();
builder.Services.AddScoped<IValidator<VehicleInsertDto>, VehicleInsertValidator>();

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repository
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();

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
