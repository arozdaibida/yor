using MediatR;

using System.Text.Json.Serialization;

using yor_database_infrastructure.Contracts;
using yor_database_infrastructure.Repositories;

using yor_request_api.Application.Configurations;
using yor_request_api.Application.Extensions;
using yor_request_api.Infrastructure.Repositories;
using yor_request_api.Infrastructure.Repositories.Contracts;
using yor_request_api.Infrastructure.RequestUnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .GonfigureJwtSettings(builder.Configuration);

builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services
    .AddJsonWebToken(builder.Configuration)
    .AddDatabaseContext(builder.Configuration)
    .AddMediatR(typeof(Program).Assembly);

builder.Services.AddSwaggerGen();

builder.Services
    .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
    .AddTransient(typeof(IRepository<>), typeof(Repository<>))
    .AddTransient<IRequestUnitOfWork, RequestUnitOfWork>();

var app = builder.Build();

app.UseCors(c =>
    c.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app
   .UseSwagger()
   .UseAuthentication()
   .UseAuthorization()
   .UseSwaggerUI();

app.MapControllers();

app.Run();