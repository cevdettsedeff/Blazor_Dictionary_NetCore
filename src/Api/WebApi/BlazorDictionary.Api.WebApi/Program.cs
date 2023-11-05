using BlazorDictionary.Infrastructure.Persistence.Extensions;
using BlazorDictionary.Api.Application.Extensions;
using FluentValidation.AspNetCore;
using FluentValidation;
using BlazorDictionary.Api.Application.Features.Commands.User.Login;
using BlazorDictionary.Api.WebApi.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {

        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
    

builder.Services.AddValidatorsFromAssemblyContaining<LoginUserCommandValidator>(); 
builder.Services.AddFluentValidationAutoValidation(); 
builder.Services.AddFluentValidationClientsideAdapters(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddInfrastructureRegistration(builder.Configuration); // Uygulama çalýþtýktan sonra gerekli iþlemleri yapar.
builder.Services.AddApplicationRegistration(); // Automapper, MediatR ve Fluent Validation kýsmýný buradaki method ile ekledik.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
