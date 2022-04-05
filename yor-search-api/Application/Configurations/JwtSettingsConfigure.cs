using yor_search_api.Infrastructure.Models;

namespace yor_search_api.Application.Configurations
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
