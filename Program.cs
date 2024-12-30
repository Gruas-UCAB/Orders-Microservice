using contractsMicroservice.src.contract.infrastructure.repositories;
using DotNetEnv;
using FluentValidation;

using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.contract.application.commands.create_contract.types;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.domain.entities.policy.infrastructure.dto;
using OrdersMicroservice.src.contract.domain.entities.policy.infrastructure.validators;
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


/*builder.Services.AddTransient<IValidator<CreatePolicyCommand>, CreatePolicyCommandValidator>();*/
builder.Services.AddTransient<IValidator<UpdatePolicyDto>, UpdatePolicyByIdValidator>();
builder.Services.AddScoped<IPolicyRepository, MongoPolicyRepository>();

builder.Services.AddTransient<IValidator<CreateExtraCostCommand>, CreateExtraCostCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateExtraCostDto>, UpdateExtraCostByIdValidator>();
/*builder.Services.AddScoped<IExtraCostRepository, MongoExtraCostRepository>();*/

/*builder.Services.AddTransient<IValidator<CreateVehicleCommand>, CreateVehicleCommandValidator>();*/
/*builder.Services.AddTransient<IValidator<UpdateVehicleDto>, UpdateVehicleIdValidator>();*/
builder.Services.AddScoped<IVehicleRepository, MongoVehicleRepository>();

builder.Services.AddTransient<IValidator<CreateContractCommand>, CreateContractCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateContractDto>, UpdateContractByIdValidator>();
builder.Services.AddScoped<IContractRepository, MongoContractRepository>();

builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseAuthentication()
   .UseAuthorization();

app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
