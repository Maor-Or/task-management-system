using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Infrastructure.Repositories;
using TaskManagement.Application.Services;
using TaskManagement.Api.Middleware;
using TaskManagement.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using TaskManagement.Application.Interfaces.Security;
using TaskManagement.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<RegisterUserService>();
builder.Services.AddScoped<LoginUserService>();
//builder.Services.AddFluentValidation(); - deprecated
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters(); // Enables automatic validation using FluentValidation.
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>(); // Scan project and register validator classes.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
