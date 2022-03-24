using yor_auth_api.Model;

namespace yor_auth_api.Application.Configures
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
