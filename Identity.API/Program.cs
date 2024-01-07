using Identity.API.Data;
using Identity.API.Models;
using Identity.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuracion de EF Core con SQLServer
var connectionString = builder.Configuration.GetConnectionString("IdentityDB");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//Inyeccion de depencia para IUserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Configurar el Identity Framework ASP.NET Core
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

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
