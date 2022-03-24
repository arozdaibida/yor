﻿using yor_auth_api.Infrastructure.Contexts;

namespace yor_auth_api.Application.Extensions
{
    public static class AddDatabaseContextExtension
    {
        public static IServiceCollection AddDatabaseContext(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options 
                => configuration.GetConnectionString(
                    Constants.EntityFrameworkOptions.ConnectionName));

            return services;
        }
    }
}
