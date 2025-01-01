using DotNetEnv;
using FluentValidation;

using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.contract.application.commands.create_contract.types;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.infrastructure.dto;
using OrdersMicroservice.src.contract.infrastructure.repositories;
using OrdersMicroservice.src.contract.infrastructure.validators;
using OrdersMicroservice.src.extracost.application.commands.create_extracost.types;
using OrdersMicroservice.src.extracost.application.repositories;
using OrdersMicroservice.src.extracost.infrastructure.dto;
using OrdersMicroservice.src.extracost.infrastructure.validators;



var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<MongoDBConfig>();
builder.Services.AddScoped<IPolicyRepository, MongoPolicyRepository>();

builder.Services.AddTransient<IValidator<CreateExtraCostCommand>, CreateExtraCostCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateExtraCostDto>, UpdateExtraCostByIdValidator>();
builder.Services.AddTransient<IValidator<CreateContractCommand>, CreateContractCommandValidator>();


builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseAuthentication()
   .UseAuthorization();

app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
