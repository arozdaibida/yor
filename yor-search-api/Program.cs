using MediatR;

using System.Text.Json.Serialization;

using yor_database_infrastructure.Contracts;
using yor_database_infrastructure.Repositories;

using yor_search_api.Application.Configurations;
using yor_search_api.Application.Extensions;
using yor_search_api.Application.Service;
using yor_search_api.Application.Service.Service;
using yor_search_api.Infrastructure.Repositories;
using yor_search_api.Infrastructure.Repositories.Contracts;
using yor_search_api.Infrastructure.SearchUnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .GonfigureJwtSettings(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services
    .AddJsonWebToken(builder.Configuration)
    .AddDatabaseContext(builder.Configuration)
    .AddSwaggerGen()
    .AddMediatR(typeof(Program).Assembly);

builder.Services
    .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
    .AddTransient(typeof(IRepository<>), typeof(Repository<>))
    .AddTransient<ISearchUnitOfWork, SearchUnitOfWork>()
    .AddTransient<ICurrentUserService, CurrentUserService>();

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

//TODO add swagger extension to add token to header