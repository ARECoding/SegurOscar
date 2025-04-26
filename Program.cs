using Microsoft.EntityFrameworkCore;
using SegurOsCar.Models;
using SegurOsCar.Services;
using SegurOsCar.DTOs;
using SegurOsCar.Repository;
using Microsoft.Data.SqlClient;
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

// deserializador de json
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new VehicleJsonDeserializer());
    });


builder.Services.AddKeyedScoped<ICommonServices<ClientDto, ClientInsertDto, ClientUpdateDto>, ClientService>("clientService");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repository
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
