using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using ContractAppAPI;
using ContractAppAPI.Data;
using ContractAppAPI.Extensions;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Middleware;
using ContractAppAPI.Repository;
using ContractAppAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IContractTypeOneRepository, ContractTypeOneRepository>();
builder.Services.AddScoped<IContractTypeTwoRepository, ContractTypeTwoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin", builder => builder.RequireClaim(ClaimTypes.Role, "Admin"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<Seeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

seeder.SeedDataContext();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
