using yor_request_api.Infrastructure.Models;

namespace yor_request_api.Application.Configurations
{
    public static class JwtSettingsConfigure
    {
        public static IServiceCollection GonfigureJwtSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtSettings>(options
                => configuration.GetSection(Constants.JwtOptions.JwtKeys).Bind(options));

            return services;
        }
    }
}
