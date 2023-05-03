using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projeto_AndreTurismoApp.AddressService.Data;
using Projeto_AndreTurismoApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Projeto_AndreTurismoAppAddressServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Projeto_AndreTurismoAppAddressServiceContext") ?? throw new InvalidOperationException("Connection string 'Projeto_AndreTurismoAppAddressServiceContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 
builder.Services.AddSingleton<PostOfficeService>();

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
