using contractsMicroservice.src.contract.infrastructure.repositories;
using DotNetEnv;
using FluentValidation;
using MassTransit;
using MassTransit.JobService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using OrdersMicroservice.core.Application;
using OrdersMicroservice.core.Infrastructure;
using OrdersMicroservice.src.contract.application.commands.create_contract.types;
using OrdersMicroservice.src.contract.application.repositories;
using OrdersMicroservice.src.contract.infrastructure.repositories;
using OrdersMicroservice.src.contract.infrastructure.validators;
using OrdersMicroservice.src.order.application.commands.create_extra_cost.types;
using OrdersMicroservice.src.order.application.repositories;
using OrdersMicroservice.src.order.infrastructure.repositories;
using OrdersMicroservice.src.order.infrastructure.state_machine;
using OrdersMicroservice.src.order.infrastructure.validators;
using RestSharp;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<MongoDBConfig>();
builder.Services.AddSingleton<IRestClient>(sp => new RestClient());
builder.Services.AddScoped<IIdGenerator<string>, UUIDGenerator>();
builder.Services.AddScoped<IPolicyRepository, MongoPolicyRepository>();
builder.Services.AddScoped<IContractRepository, MongoContractRepository>();
builder.Services.AddScoped<IOrderRepository, MongoOrderRepository>();
builder.Services.AddScoped<IExtraCostRepository, MongoExtraCostRepository>();
builder.Services.AddTransient<IValidator<CreateExtraCostCommand>, CreateExtraCostCommandValidator>();
builder.Services.AddTransient<IValidator<CreateContractCommand>, CreateContractCommandValidator>();
builder.Services.Configure<FirebaseMessagingSettings>(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("FIREBASE_API_KEY");
    options.SenderId = Environment.GetEnvironmentVariable("FIREBASE_SENDER_ID");
    options.ProjectId = Environment.GetEnvironmentVariable("FIREBASE_PROJECT_ID");
});
builder.Services.AddSingleton<IMessagingService, FirebaseNotificationSender>();
builder.Services.AddSingleton<IMessagingClient, FirebaseMessagingClient>();
builder.Services.AddSingleton<INotificationAppClient, FirebaseAppClient>();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumers(typeof(Program).Assembly);

    busConfigurator.AddSagaStateMachine<OrderStatusSaga, OrderStatusSagaData>()
    .MongoDbRepository(r =>
    {
        r.Connection = Environment.GetEnvironmentVariable("MONGODB_CNN");
        r.DatabaseName = Environment.GetEnvironmentVariable("MONGODB_MASSTRANSIT_NAME");
        r.CollectionName = "orderStatusSaga";
    });

    BsonClassMap.RegisterClassMap<OrderStatusSagaData>(cm =>
    {
        cm.AutoMap();
        cm.MapIdProperty(x => x.CorrelationId)
            .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
    });

    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(Environment.GetEnvironmentVariable("RABBITMQ_HOST")!), hst =>
        {
            hst.Username(Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest");
            hst.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest");
        });
        cfg.UseInMemoryOutbox(context);
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY")!)),
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreationalOrderUser", policy => policy.RequireClaim("UserRole", ["admin", "operator"]));
    options.AddPolicy("AdminUser", policy => policy.RequireClaim("UserRole", "admin"));
});
builder.Services.AddScoped<ITokenAuthenticationService, JwtService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Orders Microservice API",
        Description = "API documentation for Orders Microservice"
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders Microservice API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
