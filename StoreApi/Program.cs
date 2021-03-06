using Microsoft.EntityFrameworkCore;
using StoreApi.Contracts;
using StoreApi.Models;
using StoreApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreApiDb")));

#region Repository
    builder.Services.AddTransient<ICustomersRepository, CustomersRepository>();
    builder.Services.AddTransient<IProductsRepository, ProductsRepository>();
    builder.Services.AddTransient<ISalesPeopleRepository, SalesPeopleRepository>();
#endregion

builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

app.Run();