using DotNetEnv;
using FluentValidation;

using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.extracost.application.commands.create_extracost.types;
using OrdersMicroservice.src.extracost.application.repositories;
using OrdersMicroservice.src.extracost.infrastructure.dto;
using OrdersMicroservice.src.extracost.infrastructure.repositories;
using OrdersMicroservice.src.extracost.infrastructure.validators;
using OrdersMicroservice.src.policy.application.commands.create_policy.types;
using OrdersMicroservice.src.policy.application.repositories;
using OrdersMicroservice.src.policy.infrastructure.dto;
using OrdersMicroservice.src.policy.infrastructure.repositories;
using OrdersMicroservice.src.policy.infrastructure.validators;
using OrdersMicroservice.src.policyt.infrastructure.validators;
using OrdersMicroservice.src.vehicle.application.commands.create_vehicle.types;
using OrdersMicroservice.src.vehicle.application.repositories;
using OrdersMicroservice.src.vehicle.infrastructure.repositories;
using OrdersMicroservice.src.vehicle.infrastructure.validators;
using UsersMicroservice.core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<MongoDBConfig>();


builder.Services.AddTransient<IValidator<CreatePolicyCommand>, CreatePolicyCommandValidator>();
builder.Services.AddTransient<IValidator<UpdatePolicyDto>, UpdatePolicyByIdValidator>();
builder.Services.AddScoped<IPolicyRepository, MongoPolicyRepository>();

builder.Services.AddTransient<IValidator<CreateExtraCostCommand>, CreateExtraCostCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateExtraCostDto>, UpdateExtraCostByIdValidator>();
builder.Services.AddScoped<IExtraCostRepository, MongoExtraCostRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSingleton<MongoDBConfig>();
builder.Services.AddTransient<IValidator<CreateVehicleCommand>, CreateVehicleCommandValidator>();
builder.Services.AddScoped<IVehicleRepository, MongoVehicleRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseAuthentication()
   .UseAuthorization();

app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
