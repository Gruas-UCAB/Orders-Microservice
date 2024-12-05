using DotNetEnv;
using FluentValidation;

using OrdersMicroservice.core.Infrastructure;

using OrdersMicroservice.src.policy.application.commands.create_policy.types;
using OrdersMicroservice.src.policy.application.repositories;
using OrdersMicroservice.src.policy.infrastructure.dto;
using OrdersMicroservice.src.policy.infrastructure.repositories;
using OrdersMicroservice.src.policy.infrastructure.validators;
using OrdersMicroservice.src.policyt.infrastructure.validators;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<MongoDBConfig>();


builder.Services.AddTransient<IValidator<CreatePolicyCommand>, CreatePolicyCommandValidator>();
builder.Services.AddTransient<IValidator<UpdatePolicyDto>, UpdatePolicyByIdValidator>();
builder.Services.AddScoped<IPolicyRepository, MongoPolicyRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseAuthentication()
   .UseAuthorization();

app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
