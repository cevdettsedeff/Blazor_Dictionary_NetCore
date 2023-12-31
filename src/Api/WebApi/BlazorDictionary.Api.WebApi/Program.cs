using BlazorDictionary.Infrastructure.Persistence.Extensions;
using BlazorDictionary.Api.Application.Extensions;
using FluentValidation.AspNetCore;
using FluentValidation;
using BlazorDictionary.Api.Application.Features.Commands.User.Login;
using BlazorDictionary.Api.WebApi.Infrastructure.Extensions;
using BlazorDictionary.Api.WebApi.Infrastructure.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => opt.Filters.Add<ValidateModelStateFilter>())
    .AddJsonOptions(opt =>
    {

        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    }).ConfigureApiBehaviorOptions( opt => opt.SuppressModelStateInvalidFilter = true);
    

builder.Services.AddValidatorsFromAssemblyContaining<LoginUserCommandValidator>(); 
builder.Services.AddFluentValidationAutoValidation(); 
builder.Services.AddFluentValidationClientsideAdapters(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddInfrastructureRegistration(builder.Configuration); // Uygulama �al��t�ktan sonra gerekli i�lemleri yapar.
builder.Services.AddApplicationRegistration(); // Automapper, MediatR ve Fluent Validation k�sm�n� buradaki method ile ekledik.

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

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

app.UseCors("MyPolicy");

app.MapControllers();

app.Run();
