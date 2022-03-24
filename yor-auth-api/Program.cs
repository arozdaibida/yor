using FastEndpoints;
using FastEndpoints.Swagger;

using MediatR;

using yor_auth_api.Application.Configures;
using yor_auth_api.Application.Extensions;
using yor_auth_api.Application.Services;
using yor_auth_api.Application.Services.Services;
using yor_auth_api.Infrastructure.AuthUnitOfWork;
using yor_auth_api.Infrastructure.Contracts;
using yor_auth_api.Infrastructure.Repositories;

using yor_database_infrastructure.Contracts;
using yor_database_infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .GonfigureJwtSettings(builder.Configuration);

builder.Services
    .AddJsonWebToken(builder.Configuration)
    .AddDatabaseContext(builder.Configuration)
    .AddSwaggerDoc()
    .AddFastEndpoints()
    .AddMediatR(typeof(Program).Assembly);

builder.Services
    .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
    .AddTransient<IAuthRepository, AuthRepository>()
    .AddTransient<IAuthUnitOfWork, AuthUnitOfWork>()
    .AddTransient<IAuthService, AuthServices>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(c => c.ConfigureDefaults());

app.Run();
